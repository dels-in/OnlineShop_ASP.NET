using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class SearchController : Controller
{
    private readonly ProductStorage _productStorage;

    public SearchController(ProductStorage productStorage)
    {
        _productStorage = productStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    public RedirectToActionResult Details(string productName)
    {
        if (string.IsNullOrEmpty(productName))
            return RedirectToAction("Index");
        var product = _productStorage.GetProduct(productName);
        return RedirectToAction("Details", "Product", new { productId = product.Id });
    }
}