using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class ComparitionController : Controller
{
    private readonly IStorage<Comparition, Product> _inMemoryComparitionStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public ComparitionController(IStorage<Comparition, Product> inMemoryComparitionStorage,
        IProductStorage inMemoryProductStorage)
    {
        _inMemoryComparitionStorage = inMemoryComparitionStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryComparitionStorage.GetByUserId(GetUserId()));
    }

    public IActionResult AddToComparition(int productId)
    {
        _inMemoryComparitionStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        return RedirectToAction("Index", "Product");
    }

    public IActionResult AddToComparitionDetails(int productId)
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