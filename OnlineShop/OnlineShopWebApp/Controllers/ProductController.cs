using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    private readonly ProductStorage _productStorage;

    public ProductController(ProductStorage productStorage)
    {
        _productStorage = productStorage;
    }

    public IActionResult Index()
    {
        return View(_productStorage.GetAll());
    }

    public IActionResult Details(int productId)
    {
        var product = _productStorage.GetProduct(productId);
        return View(product);
    }
}