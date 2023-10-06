using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class SearchController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public RedirectToActionResult Details(string name)
    {
        if (string.IsNullOrEmpty(name)) 
            return RedirectToAction("Index");
        return RedirectToAction("Details", "Product",name);
    }
}