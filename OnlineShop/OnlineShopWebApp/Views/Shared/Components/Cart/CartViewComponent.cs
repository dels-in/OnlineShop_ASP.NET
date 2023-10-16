using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Views.Shared.Components;

public class CartViewComponent : ViewComponent
{
    private readonly IStorage<Cart, Product> _inMemoryCartsStorage;

    public CartViewComponent(IStorage<Cart, Product> inMemoryCartsStorage)
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
        return feature.AnonymousId ?? "007";
    }
}