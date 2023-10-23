using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    private readonly IPRStorage<Product> _inMemoryProductStorage;

    public ProductController(IPRStorage<Product> inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryProductStorage.GetAll());
    }

    public IActionResult Details(int productId)
    {
        var product = _inMemoryProductStorage.Get(productId);
        return View(product);
    }
}