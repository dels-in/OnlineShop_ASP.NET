using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Views.Shared.Components.Comparison;

public class ComparisonViewComponent : ViewComponent
{
    private readonly IStorage<OnlineShop.Db.Models.Comparison, Product> _comparisonDbStorage;

    public ComparisonViewComponent(IStorage<OnlineShop.Db.Models.Comparison, Product> comparisonDbStorage)
    {
        _comparisonDbStorage = comparisonDbStorage;
    }

    public IViewComponentResult Invoke()
    {
        var comparison = _comparisonDbStorage.GetByUserId(GetUserId());
        var productsCount =  comparison?.Products?.Count ?? 0;
        return View("Comparison", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}