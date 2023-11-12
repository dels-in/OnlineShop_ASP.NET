using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    private readonly IStorage<Cart, ProductViewModel> _inMemoryCartsStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public CartController(IStorage<Cart, ProductViewModel> inMemoryCartsStorage, IProductStorage inMemoryProductStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryCartsStorage.GetByUserId(GetUserId()));
    }

    public IActionResult AddToCartRedirect(Guid productId)
    {
        _inMemoryCartsStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult AddToCartStay(Guid productId)
    {
        _inMemoryCartsStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(Guid productId)
    {
        _inMemoryCartsStorage.Reduce(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid productId)
    {
        _inMemoryCartsStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}