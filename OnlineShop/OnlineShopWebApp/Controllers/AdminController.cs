using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class AdminController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;

    public AdminController(IProductStorage inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Orders()
    {
        return View();
    }

    public IActionResult Users()
    {
        return View();
    }

    public IActionResult Roles()
    {
        return View();
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
    public RedirectToActionResult Add(string productName, decimal productCost,
        string productDescription, string productSource,
        int productMetacriticScore, string productGenre)
    {
        var product = new Product(productName, productCost, productDescription, productSource, productMetacriticScore,
            productGenre);
        _inMemoryProductStorage.Add(product);
        return RedirectToAction("Products");
    }

    public IActionResult EditProduct()
    {
        return View();
    }

    [HttpPost]
    public RedirectToActionResult Edit(int productId, string productName, decimal productCost,
        string productDescription, string productSource,
        int productMetacriticScore, string productGenre)
    {
        _inMemoryProductStorage.Edit(productId, productName, productCost, productDescription, productSource,
            productMetacriticScore, productGenre);
        return RedirectToAction("Products");
    }

    public RedirectToActionResult DeleteProduct(int productId)
    {
        _inMemoryProductStorage.Delete(productId);
        return RedirectToAction("Products");
    }
}