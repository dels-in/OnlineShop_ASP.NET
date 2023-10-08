using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;

    public ProductController(IProductStorage inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryProductStorage.GetAll());
    }

    public IActionResult Details(int productId)
    {
        var product = _inMemoryProductStorage.GetProduct(productId);
        return View(product);
    }
}