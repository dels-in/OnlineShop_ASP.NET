using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "007";
        return View(CartStorage.GetAll(userId).OrderBy(p=> p.Name));
    }

    public RedirectToActionResult AddToCartRedirect(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? "007";
        CartStorage.AddToCart(id, userId);
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToCartStay(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? "007";
        CartStorage.AddToCart(id, userId);
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? "007";
        CartStorage.Reduce(id, userId);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? "007";
        CartStorage.Delete(id, userId);
        return RedirectToAction("Index");
    }
}