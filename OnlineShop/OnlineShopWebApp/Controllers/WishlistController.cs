using AspNetCore.Unobtrusive.Ajax;
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

    public IActionResult Delete(int productId)
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

    public IActionResult DeleteAndRedirectToCart(int productId)
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

    [HttpPost]
    [AjaxOnly]
    public IActionResult AddToWishlist(int productId)
    {
        _wishlistDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        return PartialView("_IconsPartial");
    }

    [HttpPost]
    [AjaxOnly]
    public IActionResult AddToWishlistDetails(int productId)
    {
        try
        {
            _wishlistDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return PartialView("_IconsPartial");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}