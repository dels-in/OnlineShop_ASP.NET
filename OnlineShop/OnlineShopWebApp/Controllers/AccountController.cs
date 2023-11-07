using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.Controllers;
using WebApplication1.Authentications;
using WebApplication1.Models;
using WebApplication1.Storages;
using Account = WebApplication1.Models.Account;

namespace WebApplication1.Controllers;

public class AccountController : Controller
{
    private readonly IAccountStorage _inMemoryAccountStorage;
    private readonly IUserInfoStorage _inMemoryUserInfoStorage;

    public AccountController(IAccountStorage inMemoryAccountStorage, IUserInfoStorage inMemoryUserInfoStorage)
    {
        _inMemoryAccountStorage = inMemoryAccountStorage;
        _inMemoryUserInfoStorage = inMemoryUserInfoStorage;
    }

    public IActionResult Details()
    {
        return View("Details");
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
            return View(login);
        }

        if (login.Password != account.Password)
        {
            ModelState.AddModelError("", "Your password is incorrect");
        }

        if (!ModelState.IsValid)
        {
            return View(login);
        }

        return RedirectToAction("Details");
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
        return RedirectToAction("Details");
    }

    public IActionResult GithubLogin()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/Account/AppAdd" }, "Github");
    }

    public IActionResult GoogleLogin()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/Account/AppAdd" }, "Google");
    }

    public IActionResult GoogleAdd()
    public IActionResult AppAdd()
    {
        var userId = Guid.NewGuid();
        var email = AppLogin.Email;
        var password = Guid.NewGuid().ToString().Substring(1, 8);
        _inMemoryAccountStorage.AddToList(new Account
        {
            UserId = userId,
            Email = email,
            Password = password,
            ConfirmPassword = password,
            RoleName = "User"
        });
        _inMemoryUserInfoStorage.AddToList(new UserInfo
        {
            UserId = userId,
            FirstName = AppLogin.FirstName,
            LastName = AppLogin.LastName,
            Address = null,
            Address2 = null,
            Email = email,
            City = null,
            PostCode = null,
            Region = null,
        });
        return RedirectToAction("Details");
    }
}