using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Views.Shared.Components.Wishlist;

public class WishlistViewComponent : ViewComponent
{
    private readonly IStorage<OnlineShop.Db.Models.Wishlist, Product> _wishlistDbStorage;

    public WishlistViewComponent(IStorage<OnlineShop.Db.Models.Wishlist, Product> wishlistDbStorage)
    {
        _wishlistDbStorage = wishlistDbStorage;
    }

    public IViewComponentResult Invoke()
    {
        var wishlist = _wishlistDbStorage.GetByUserId(GetUserId());
        var productsCount = wishlist?.Products?.Count ?? 0;
        return View("Wishlist", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}