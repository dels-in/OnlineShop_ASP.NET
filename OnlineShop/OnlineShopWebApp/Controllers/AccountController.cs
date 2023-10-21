using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class AccountController : Controller
{
    private readonly IAccountStorage _inMemoryAccountStorage;

    public AccountController(IAccountStorage inMemoryAccountStorage)
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
        if (account.Email == account.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (ModelState.IsValid)
        {
            _inMemoryAccountStorage.AddToList(account);
            return View();
        }

        return View();
    }
}