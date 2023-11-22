using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Controllers;

public class CartController : Controller
{
    private readonly IStorage<Cart, Product> _cartsDbStorage;
    private readonly IProductStorage _productDbStorage;

    public CartController(IStorage<Cart, Product> cartsDbStorage,
        IProductStorage productDbStorage)
    {
        _cartsDbStorage = cartsDbStorage;
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<CartViewModel, Cart>.ToViewModel(_cartsDbStorage.GetByUserId(GetUserId())));
    }

    public IActionResult AddToCartRedirect(int productId)
    {
        _cartsDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult AddToCartStay(int productId)
    {
        _cartsDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int productId)
    {
        _cartsDbStorage.Reduce(_productDbStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int productId)
    {
        _cartsDbStorage.Delete(_productDbStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}