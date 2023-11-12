using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace WebApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;

    public ProductsController(IProductStorage inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryProductStorage.GetAll());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _inMemoryProductStorage.Add(product);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid productId)
    {
        return View(_inMemoryProductStorage.GetProduct(productId));
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _inMemoryProductStorage.Edit(product);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid productId)
    {
        _inMemoryProductStorage.Delete(productId);
        return RedirectToAction("Index");
    }
}