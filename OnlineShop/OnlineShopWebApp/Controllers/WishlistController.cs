using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class WishlistController : Controller
{
    private readonly IStorage<Wishlist, Product> _inMemoryWishlistStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public WishlistController(IStorage<Wishlist, Product> inMemoryWishlistStorage,
        IProductStorage inMemoryProductStorage)
    {
        _inMemoryWishlistStorage = inMemoryWishlistStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryWishlistStorage.GetByUserId(GetUserId()));
    }

    public IActionResult Delete(int productId)
    {
        try
        {
            _inMemoryWishlistStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index");
    }

    public IActionResult DeleteAndRedirectToCart(int productId)
    {
        try
        {
            _inMemoryWishlistStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("AddToCartRedirect", "Cart", new { productId });
    }

    public IActionResult AddToWishlist(int productId)
    {
        try
        {
            _inMemoryWishlistStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index", "Product");
    }

    public IActionResult AddToWishlistDetails(int productId)
    {
        try
        {
            _inMemoryWishlistStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Details", "Product", new { productId });
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}