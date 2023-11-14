using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers;

public class HomeController : Controller
{
    private readonly IProductStorage _productDbStorage;

    public HomeController(IProductStorage productDbStorage)
    {
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModelList(_productDbStorage.GetAll()));
    }

    public IActionResult Privacy()
    {
        return View();
    }
}