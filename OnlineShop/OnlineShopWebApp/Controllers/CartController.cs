using AspNetCore.Unobtrusive.Ajax;
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

    [HttpPost]
    public IActionResult Add(int productId, string userId)
    {
        _cartsDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        var cart = _cartsDbStorage.GetByUserId(userId);
        var cartViewModel = Mapping<CartViewModel, Cart>.ToViewModel(cart);
        return PartialView("_CartItemQuantity", cartViewModel);
    }

    [HttpPost]
    [AjaxOnly]
    public IActionResult AddToCartStay(int productId)
    {
        _cartsDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("PartialIndex", "Product");
    }

    [HttpPost]
    public IActionResult Reduce(int productId, string userId)
    {
        _cartsDbStorage.Reduce(_productDbStorage.GetProduct(productId), GetUserId());
        var cart = _cartsDbStorage.GetByUserId(userId);
        var cartViewModel = Mapping<CartViewModel, Cart>.ToViewModel(cart);
        return PartialView("_CartItemQuantity", cartViewModel);
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