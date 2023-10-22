using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    private readonly IStorage<Cart, Product> _inMemoryCartsStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public CartController(IStorage<Cart, Product> inMemoryCartsStorage, IProductStorage inMemoryProductStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryCartsStorage.GetByUserId(GetUserId()));
    }

    public IActionResult AddToCartRedirect(int productId)
    {
        _inMemoryCartsStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult AddToCartStay(int productId)
    {
        _inMemoryCartsStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Reduce(int productId)
    {
        _inMemoryCartsStorage.Reduce(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int productId)
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