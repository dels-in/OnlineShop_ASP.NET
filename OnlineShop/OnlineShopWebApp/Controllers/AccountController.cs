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
        if (login.Email != account.Email)
        {
            ModelState.AddModelError("", "There is no such account");
            return RedirectToAction("Login");
        }

        if (login.Password != account.Password)
        {
            ModelState.AddModelError("", "Your password is incorrect");
            return RedirectToAction("Login");
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Model is not valid");
            return View(login);
        }

        login.UserId = account.UserId;
        return View("Details");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Register register)
    {
        if (register.Email == register.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (!ModelState.IsValid)
        {
            return View(register);
        }

        _inMemoryAccountStorage.AddToList(register);
        return View("Details");
    }
}