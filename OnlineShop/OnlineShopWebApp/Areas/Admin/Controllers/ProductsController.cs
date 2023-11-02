using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;

    public ProductsController(IProductStorage inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Products()
    {
        return View(_inMemoryProductStorage.GetAll());
    }

    public IActionResult AddNewProduct()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _inMemoryProductStorage.Add(product);
        return RedirectToAction("Products");
    }

    public IActionResult EditProduct(Guid productId)
    {
        return View(_inMemoryProductStorage.GetProduct(productId));
    }

    [HttpPost]
    public IActionResult EditProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _inMemoryProductStorage.Edit(product);
        return RedirectToAction("Products");
    }

    public IActionResult DeleteProduct(Guid productId)
    {
        _inMemoryProductStorage.Delete(productId);
        return RedirectToAction("Products");
    }
}