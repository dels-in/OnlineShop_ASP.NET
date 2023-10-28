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
    public IActionResult DetailsRegister(Register register)
    {
        if (register.Email == register.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (ModelState.IsValid)
        {
            _inMemoryAccountStorage.AddToList(register);
            return View("Details", register);
        }

        return RedirectToAction("Register");
    }

    [HttpPost]
    public IActionResult DetailsLogin(Login login)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Model is not valid");
            return RedirectToAction("Login");
        }

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

        login.UserId = account.UserId;
        return View("Details");
    }
}