using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductStorage _productDbStorage;

    public ProductController(IProductStorage productDbStorage)
    {
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModelList(_productDbStorage.GetAll()));
    }

    public IActionResult Details(int productId)
    {
        var product = _productDbStorage.GetProduct(productId);
        return View(Mapping<ProductViewModel, Product>.ToViewModel(product));
    }
}