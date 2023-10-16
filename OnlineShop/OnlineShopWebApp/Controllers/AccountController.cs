using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class AccountController: Controller
{
    private readonly IStorage<Account, Account> _inMemoryAccountStorage;

    public AccountController(IStorage<Account, Account> inMemoryAccountStorage)
    {
        _inMemoryAccountStorage = inMemoryAccountStorage;
    }

    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Details(Account account)
    {
        _inMemoryAccountStorage.AddToList(account, GetUserId());
        return View();
    }
    
    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
    
}