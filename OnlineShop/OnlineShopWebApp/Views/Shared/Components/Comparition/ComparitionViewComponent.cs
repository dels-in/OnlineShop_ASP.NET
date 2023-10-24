using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Views.Shared.Components;

public class ComparitionViewComponent : ViewComponent
{
    private readonly IStorage<Comparition, Product> _inMemoryComparitionStorage;

    public ComparitionViewComponent(IStorage<Comparition, Product> inMemoryComparitionStorage)
    {
        _inMemoryComparitionStorage = inMemoryComparitionStorage;
    }

    public IViewComponentResult Invoke()
    {
        var comparition = _inMemoryComparitionStorage.GetByUserId(GetUserId());
        var productsCount = comparition?.Products.Count ?? 0;
        return View("Comparition", productsCount);
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}