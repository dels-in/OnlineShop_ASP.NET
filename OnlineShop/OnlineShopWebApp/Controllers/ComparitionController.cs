using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ComparitionController : Controller
{
    private readonly IStorage<Comparition> _inMemoryComparitionStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public ComparitionController(IStorage<Comparition> inMemoryComparitionStorage, IProductStorage inMemoryProductStorage)
    {
        _inMemoryComparitionStorage = inMemoryComparitionStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryComparitionStorage.GetByUserId(GetUserId()));
    }

    public RedirectToActionResult AddToComparition(int productId)
    {
        _inMemoryComparitionStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public RedirectToActionResult AddToComparitionDetails(int productId)
    {
        _inMemoryComparitionStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Details", "Product", new { productId });
    }

    public IActionResult Delete(int productId)
    {
        _inMemoryComparitionStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}