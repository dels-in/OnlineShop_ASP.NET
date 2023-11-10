using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class ComparisonController : Controller
{
    private readonly IStorage<Comparison, Product> _inMemoryComparisonStorage;
    private readonly IProductStorage _inMemoryProductStorage;

    public ComparisonController(IStorage<Comparison, Product> inMemoryComparisonStorage,
        IProductStorage inMemoryProductStorage)
    {
        _inMemoryComparisonStorage = inMemoryComparisonStorage;
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryComparisonStorage.GetByUserId(GetUserId()));
    }

    public IActionResult AddToComparison(Guid productId)
    {
        try
        {
            _inMemoryComparisonStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Index", "Product");
    }

    public IActionResult AddToComparisonDetails(Guid productId)
    {
        try
        {
            _inMemoryComparisonStorage.AddToList(_inMemoryProductStorage.GetProduct(productId), GetUserId());
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
            _inMemoryComparisonStorage.Delete(_inMemoryProductStorage.GetProduct(productId), GetUserId());
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