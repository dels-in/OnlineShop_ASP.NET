using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Views.Shared.Components.Cart;

public class CartViewComponent : ViewComponent
{
    private readonly IStorage<Models.Cart, Product> _inMemoryCartsStorage;

    public CartViewComponent(IStorage<Models.Cart, Product> inMemoryCartsStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
    }

    public IViewComponentResult Invoke()
    {
        var cart = _inMemoryCartsStorage.GetByUserId(GetUserId());
        var productsCount = cart?.Quantity ?? 0;
        return View("Cart", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}