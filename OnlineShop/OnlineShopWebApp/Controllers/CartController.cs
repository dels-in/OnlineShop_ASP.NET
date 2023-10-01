using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View(CartStorage.GetAll());
    }
    
    public RedirectToActionResult AddToCart(int id)
    {
        CartStorage.AddToCart(id);
        return RedirectToAction("Index");
    }
}