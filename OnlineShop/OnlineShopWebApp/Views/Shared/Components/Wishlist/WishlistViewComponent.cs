using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Views.Shared.Components;

public class WishlistViewComponent : ViewComponent
{
    private readonly IStorage<Wishlist, Product> _inMemoryWishlistStorage;

    public WishlistViewComponent(IStorage<Wishlist, Product> inMemoryWishlistStorage)
    {
        _inMemoryWishlistStorage = inMemoryWishlistStorage;
    }

    public IViewComponentResult Invoke()
    {
        var wishlist = _inMemoryWishlistStorage.GetByUserId(GetUserId());
        var productsCount = wishlist?.Products.Count ?? 0;
        return View("Wishlist", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}