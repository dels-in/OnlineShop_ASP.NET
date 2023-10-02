using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View(CartStorage.GetAll(GetUserId()).OrderBy(p => p.Name));
    }

    public RedirectToActionResult AddToCartRedirect(int id)
    {
        CartStorage.AddToCart(id, GetUserId());
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToCartStay(int id)
    {
        CartStorage.AddToCart(id, GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int id)
    {
        CartStorage.Reduce(id, GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        CartStorage.Delete(id, GetUserId());
        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}