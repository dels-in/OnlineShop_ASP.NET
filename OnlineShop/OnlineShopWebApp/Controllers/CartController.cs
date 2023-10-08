using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    private readonly CartStorage _cartStorage;
    private readonly ProductStorage _productStorage;
    public CartController(CartStorage cartStorage, ProductStorage productStorage)
    {
        _cartStorage = cartStorage;
        _productStorage = productStorage;
    }
    public IActionResult Index() 
    {
        return View(_cartStorage.GetByUserId(GetUserId()));
    }

    public RedirectToActionResult AddToCartRedirect(int productId)
    {
        _cartStorage.AddToCart(_productStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToCartStay(int productId)
    {
        _cartStorage.AddToCart(_productStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int productId)
    {
        _cartStorage.Reduce(_productStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int productId)
    {
        _cartStorage.Delete(_productStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}