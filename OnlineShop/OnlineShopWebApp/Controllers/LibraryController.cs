using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class LibraryController : Controller
{
    private readonly IStorage<Cart, Product> _inMemoryCartsStorage;
    private readonly IStorage<Library, Product> _inMemoryLibraryStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public LibraryController(IStorage<Cart, Product> inMemoryCartsStorage,
        IStorage<Library, Product> inMemoryLibraryStorage,
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

    public IActionResult AddToLibrary()
    {
        var cart = _inMemoryCartsStorage.GetByUserId(GetUserId());
        foreach (var item in cart.CartItems)
        {
            _inMemoryLibraryStorage.AddToList(_inMemoryProductStorage.GetProduct(item.Product.Id), GetUserId());
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