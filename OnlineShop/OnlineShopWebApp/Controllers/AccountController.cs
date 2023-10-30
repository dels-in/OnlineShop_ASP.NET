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

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DetailsRegister(Account account)
    {
        if (account.Email == account.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Register");
        }

        _inMemoryAccountStorage.AddToList(account);
        return View("Details", account);
    }

    [HttpPost]
    public IActionResult DetailsLogin(Login login)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Model is not valid");
            return RedirectToAction("Login", login);
        }

        var account = _inMemoryAccountStorage.GetAccount(login.Email);
        
        if (account == null)
        {
            ModelState.AddModelError("", "There is no such account");
            return RedirectToAction("Login", login);
        }

        if (login.Password != account.Password)
        {
            ModelState.AddModelError("", "Your password is incorrect");
            return RedirectToAction("Login", login);
        }
        
        return View("Details");
    }
}