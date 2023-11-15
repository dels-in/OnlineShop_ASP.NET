using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Views.Shared.Components.Cart;

public class CartViewComponent : ViewComponent
{
    private readonly IStorage<OnlineShop.Db.Models.Cart, Product> _cartsDbStorage;

    public CartViewComponent(IStorage<OnlineShop.Db.Models.Cart, Product> cartsDbStorage)
    {
        _cartsDbStorage = cartsDbStorage;
    }

    public IViewComponentResult Invoke()
    {
        var cart = Mapping<CartViewModel, OnlineShop.Db.Models.Cart>.ToViewModel(_cartsDbStorage.GetByUserId(GetUserId()));
        var productsCount =  cart?.Quantity ?? 0;
        return View("Cart", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}