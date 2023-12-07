using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;


namespace OnlineShopWebApp.Controllers;

[Authorize]
public class LibraryController : Controller
{
    private readonly IStorage<Cart, Product> _cartsDbStorage;
    private readonly IStorage<Library, Product> _libraryDbStorage;
    private readonly IProductStorage _productDbStorage;

    public LibraryController(IStorage<Cart, Product> cartsDbStorage,
        IStorage<Library, Product> libraryDbStorage,
        IProductStorage productDbStorage)
    {
        _cartsDbStorage = cartsDbStorage;
        _libraryDbStorage = libraryDbStorage;
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<LibraryViewModel, Library>.ToViewModel(_libraryDbStorage.GetByUserId(GetUserId())));
    }

    public IActionResult AddToLibrary(string userId)
    {
        userId = GetUserId();
        var cart = _cartsDbStorage.GetByUserId(userId);
        foreach (var item in cart.CartItems)
        {
            _libraryDbStorage.AddToList(_productDbStorage.GetProduct(item.Product.Id), userId);
        }

        _cartsDbStorage.Clear(cart);
        return RedirectToAction("Index", "Home");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}