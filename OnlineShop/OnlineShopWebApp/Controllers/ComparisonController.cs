using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Controllers;

public class ComparisonController : Controller
{
    private readonly IStorage<Comparison, Product> _comparisonDbStorage;
    private readonly IProductStorage _productDbStorage;

    public ComparisonController(IStorage<Comparison, Product> comparisonDbStorage,
        IProductStorage productDbStorage)
    {
        _comparisonDbStorage = comparisonDbStorage;
        _productDbStorage = productDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<ComparisonViewModel, Comparison>.ToViewModel(_comparisonDbStorage.GetByUserId(GetUserId())));
    }

    public IActionResult AddToComparison(Guid productId)
    {
        try
        { 
            _comparisonDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
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
            _comparisonDbStorage.AddToList(_productDbStorage.GetProduct(productId), GetUserId());
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
            _comparisonDbStorage.Delete(_productDbStorage.GetProduct(productId), GetUserId());
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