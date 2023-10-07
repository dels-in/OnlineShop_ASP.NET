using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View(ProductStorage.GetAll());
    }

    public IActionResult Details(int productId)
    {
        var product = ProductStorage.GetProduct(productId);
        return View(product);
    }
}