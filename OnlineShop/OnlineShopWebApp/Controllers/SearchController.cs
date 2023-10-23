using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class SearchController : Controller
{
    private readonly IPRStorage<Product> _inMemoryProductStorage;

    public SearchController(IPRStorage<Product> inMemoryProductStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Details(string searchChars)
    {
        var productsToSearch = _inMemoryProductStorage.GetAll().Where(p => p.Name.Contains(searchChars.ToUpper())).ToList();
        return View(productsToSearch);
    }
}