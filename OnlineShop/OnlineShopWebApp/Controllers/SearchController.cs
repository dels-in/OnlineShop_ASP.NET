using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class SearchController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public RedirectToActionResult Details(string productName)
    {
        if (string.IsNullOrEmpty(productName)) 
            return RedirectToAction("Index");
        var product = ProductStorage.GetProduct(productName);
        return RedirectToAction("Details", "Product", new {productId=product.Id});
    }
}