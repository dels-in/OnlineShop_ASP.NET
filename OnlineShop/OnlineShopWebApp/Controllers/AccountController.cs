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
            return View("Details");
        }
        
        return RedirectToAction("Register");
    }

     [HttpPost]
     public IActionResult DetailsLogin(Login login)
     {
         if (login.Email == login.Password)
         {
             ModelState.AddModelError("", "Email and password must not match");
         }

         if (ModelState.IsValid)
         {
             _inMemoryAccountStorage.AddToList(login);
             return View("Details");
         }

         return RedirectToAction("Login");
     }
}