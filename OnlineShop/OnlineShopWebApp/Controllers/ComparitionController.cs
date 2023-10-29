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

    public IActionResult AddToComparition(Guid productId)
    {
        try
        {
            _inMemoryComparitionStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index", "Product");
    }

    public IActionResult AddToComparitionDetails(Guid productId)
    {
        try
        {
            _inMemoryComparitionStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Details", "Product", new { productId });
    }

    public IActionResult Delete(Guid productId)
    {
        try
        {
            _inMemoryComparitionStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}