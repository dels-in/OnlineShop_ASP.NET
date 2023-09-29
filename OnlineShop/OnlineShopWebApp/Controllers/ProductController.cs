using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View(ProductStorage.GetAll());
    }

    public IActionResult Details(string name)
    {
        var product = ProductStorage.GetProduct(name);
        if (product == null)
            return new NotFoundResult();
        return View(product);
    }
}