using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers;

public class SearchController : Controller
{
    private readonly IProductStorage _productDbStorage;

    public SearchController(IProductStorage productDbStorage)
    {
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(string searchChars)
    {
        var productsToSearch = _productDbStorage.GetAll().Where(p => p.Name.Contains(searchChars.ToUpper()))
            .ToList();
        return View(Mapping<ProductViewModel, Product>.ToViewModelList(productsToSearch));
    }
}