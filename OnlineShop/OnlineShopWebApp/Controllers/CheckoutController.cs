using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class CheckoutController : Controller
{
    private readonly IStorage<Cart, Product> _inMemoryCartsStorage;
    private readonly IStorage<Validation, Checkout> _inMemoryCheckoutStorage;


    public CheckoutController(IStorage<Cart, Product> inMemoryCartsStorage,
        IStorage<Validation, Checkout> inMemoryCheckoutStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryCheckoutStorage = inMemoryCheckoutStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ViewResult Checkout(Checkout checkout)
    {
        _inMemoryCheckoutStorage.AddToList(checkout, GetUserId());
        return View(_inMemoryCartsStorage.GetByUserId(GetUserId()));
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}