using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    private readonly ICartsStorage _inMemoryCartsStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public CartController(ICartsStorage inMemoryCartsStorage, IProductStorage inMemoryProductStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryCartsStorage.GetByUserId(GetUserId()));
    }

    public RedirectToActionResult AddToCartRedirect(int productId)
    {
        _inMemoryCartsStorage.AddToCart(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    public RedirectToActionResult AddToCartStay(int productId)
    {
        _inMemoryCartsStorage.AddToCart(_inMemoryProductStorage.GetProduct(productId), GetUserId());
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