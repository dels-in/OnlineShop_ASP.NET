using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Views.Shared.Components.Comparison;

public class ComparisonViewComponent : ViewComponent
{
    private readonly IStorage<Models.Comparison, Product> _inMemoryComparisonStorage;

    public ComparisonViewComponent(IStorage<Models.Comparison, Product> inMemoryComparisonStorage)
    {
        _inMemoryComparisonStorage = inMemoryComparisonStorage;
    }

    public IViewComponentResult Invoke()
    {
        var comparison = _inMemoryComparisonStorage.GetByUserId(GetUserId());
        var productsCount = comparison?.Products.Count ?? 0;
        return View("Comparison", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}