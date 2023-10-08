using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class SearchController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;

    public SearchController(IProductStorage inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    public RedirectToActionResult Details(string productName)
    {
        if (string.IsNullOrEmpty(productName))
            return RedirectToAction("Index");
        var product = _inMemoryProductStorage.GetProduct(productName);
        if (product == null)
        {
            return  RedirectToAction("Index");
        }
        return RedirectToAction("Details", "Product", new { productId = product.Id });
    }
}