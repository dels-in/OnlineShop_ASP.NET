using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;


namespace WebApplication1.Controllers;

public class LibraryController : Controller
{
    private readonly IStorage<Cart, ProductViewModel> _inMemoryCartsStorage;
    private readonly IStorage<Library, ProductViewModel> _inMemoryLibraryStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public LibraryController(IStorage<Cart, ProductViewModel> inMemoryCartsStorage,
        IStorage<Library, ProductViewModel> inMemoryLibraryStorage,
        IProductStorage inMemoryProductStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryLibraryStorage = inMemoryLibraryStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryLibraryStorage.GetByUserId(GetUserId()));
    }

    public IActionResult AddToLibrary(string userId)
    {
        userId = GetUserId();
        var cart = _inMemoryCartsStorage.GetByUserId(userId);
        foreach (var item in cart.CartItems)
        {
            _inMemoryLibraryStorage.AddToList(_inMemoryProductStorage.GetProduct(item.ProductViewModel.Id), userId);
        }

        _inMemoryCartsStorage.Clear(cart);
        return RedirectToAction("Index", "Home");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}