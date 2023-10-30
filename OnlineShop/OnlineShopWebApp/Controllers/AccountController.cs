using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public IActionResult Login(Login login)
    {
        var account = _inMemoryAccountStorage.GetAccount(login.Email);
        if (account == null)
        {
            ModelState.AddModelError("", "There is no such account");
        }

        if (login.Password != account.Password)
        {
            ModelState.AddModelError("", "Your password is incorrect");
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Model is not valid");
            return View(login);
        }

        return View("Details");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Account account)
    {
        if (account.Email == account.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.AddToList(account);
        return View("Details");
    }
}