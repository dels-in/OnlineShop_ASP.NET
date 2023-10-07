using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    public IActionResult Index() 
    {
        return View(CartStorage.GetByUserId(GetUserId()));
    }

    public RedirectToActionResult AddToCartRedirect(int productId)
    {
        CartStorage.AddToCart(ProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToCartStay(int productId)
    {
        CartStorage.AddToCart(ProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int productId)
    {
        CartStorage.Reduce(ProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int productId)
    {
        CartStorage.Delete(ProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}