using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class WishlistController : Controller
{
    private readonly IStorage<Wishlist> _inMemoryStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public WishlistController(IStorage<Wishlist> inMemoryStorage, IProductStorage inMemoryProductStorage)
    {
        _inMemoryStorage = inMemoryStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryStorage.GetByUserId(GetUserId()));
    }

    public IActionResult Delete(int productId)
    {
        _inMemoryStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToWishlist(int productId)
    {
        _inMemoryStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public RedirectToActionResult AddToWishlistDetails(int productId)
    {
        _inMemoryStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Details", "Product", new { productId });
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}