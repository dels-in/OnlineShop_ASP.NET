using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View(CartStorage.GetAll());
    }

    public RedirectToActionResult AddToCartRedirect(int id)
    {
        CartStorage.AddToCart(id);
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToCartStay(int id)
    {
        CartStorage.AddToCart(id);
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int id)
    {
        CartStorage.Reduce(id);
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int id)
    {
        CartStorage.Delete(id);
        return RedirectToAction("Index");
    }

    
}