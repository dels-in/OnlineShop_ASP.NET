using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IProductStorage _productDbStorage;

    public ProductsController(IProductStorage productDbStorage)
    {
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModelList(_productDbStorage.GetAll()));
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _productDbStorage.Add(Mapping<Product, ProductViewModel>.ToViewModel(product));
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid productId)
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModel(_productDbStorage.GetProduct(productId)));
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _productDbStorage.Edit(Mapping<Product, ProductViewModel>.ToViewModel(product));
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid productId)
    {
        _productDbStorage.Delete(productId);
        return RedirectToAction("Index");
    }
}