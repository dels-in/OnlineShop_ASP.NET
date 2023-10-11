using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace WebApplication1.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistStorage _inMemoryWishlistStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public WishlistController(IWishlistStorage inMemoryWishlistStorage, IProductStorage inMemoryProductStorage)
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
        _inMemoryWishlistStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToWishlist(int productId)
    {
        _inMemoryWishlistStorage.AddToWishlist(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public RedirectToActionResult AddToWishlistDetails(int productId)
    {
        _inMemoryWishlistStorage.AddToWishlist(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Details", "Product", new { productId });
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}