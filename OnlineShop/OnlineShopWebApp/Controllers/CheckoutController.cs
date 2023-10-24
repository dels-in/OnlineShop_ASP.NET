using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class CheckoutController : Controller
{
    private readonly IStorage<Cart, Product> _inMemoryCartsStorage;
    private readonly IStorage<Order, Checkout> _inMemoryCheckoutStorage;


    public CheckoutController(IStorage<Cart, Product> inMemoryCartsStorage,
        IStorage<Order, Checkout> inMemoryCheckoutStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryCheckoutStorage = inMemoryCheckoutStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Checkout checkout)
    {
        try
        {
            if (HasDigits(checkout.FirstName) || HasDigits(checkout.LastName) || HasDigits(checkout.City))
            {
                ModelState.AddModelError("", "Names or City cannot contain digits");
            }

            if (checkout.IsChecked == false)
            {
                ModelState.AddModelError("", "State does not appear to be");
            }

            if (!ModelState.IsValid) return RedirectToAction("Index");
            _inMemoryCheckoutStorage.AddToList(checkout, GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return View(_inMemoryCartsStorage.GetByUserId(GetUserId()));
    }

    public IActionResult Delete()
    {
        var cart = _inMemoryCartsStorage.GetByUserId(GetUserId());
        _inMemoryCartsStorage.Clear(cart);
        return RedirectToAction("Index", "Home");
    }
    
    private bool HasDigits(string str)
    {
        return str.Any(c => c >= '0' && c <= '9');
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}