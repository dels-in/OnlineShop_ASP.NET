using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Areas.Admin.Models;
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
    public IActionResult Index(Checkout checkout)
    {
        var userId = GetUserId();
        var cart = _inMemoryCartsStorage.GetByUserId(userId);
        try
        {
            if (HasDigits(checkout.FirstName) || HasDigits(checkout.LastName) || HasDigits(checkout.City))
            {
                ModelState.AddModelError("", "Names or City cannot contain digits");
            }
            
            if (!ModelState.IsValid)
                return View(checkout);

            _inMemoryCheckoutStorage.AddToList(checkout, cart, userId);
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Checkout", _inMemoryCartsStorage.GetByUserId(userId));
    }
    
    public IActionResult Checkout(Cart cart)
    {
        return View(cart);
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