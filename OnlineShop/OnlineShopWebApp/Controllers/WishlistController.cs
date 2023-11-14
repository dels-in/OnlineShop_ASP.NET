using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Controllers;

public class WishlistController : Controller
{
    private readonly IStorage<Wishlist, Product> _wishlistDbStorage;
    private readonly IProductStorage _productDbStorage;

    public WishlistController(IStorage<Wishlist, Product> wishlistDbStorage,
        IProductStorage productDbStorage)
    {
        _wishlistDbStorage = wishlistDbStorage;
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<WishlistViewModel, Wishlist>.ToViewModel(_wishlistDbStorage.GetByUserId(GetUserId())));
    }

    public IActionResult Delete(Guid productId)
    {
        try
        {
            _wishlistDbStorage.Delete(_productDbStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index");
    }

    public IActionResult DeleteAndRedirectToCart(Guid productId)
    {
        try
        {
            _wishlistDbStorage.Delete(_productDbStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("AddToCartRedirect", "Cart", new { productId });
    }

    public IActionResult AddToWishlist(Guid productId)
    {
        try
        {
            _wishlistDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index", "Product");
    }

    public IActionResult AddToWishlistDetails(Guid productId)
    {
        try
        {
            _wishlistDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
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