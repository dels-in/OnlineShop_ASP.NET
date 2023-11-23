using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Authentications;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountStorage _accountDbStorage;
    private readonly IUserInfoStorage _userInfoDbStorage;

    public AccountController(IAccountStorage accountDbStorage, IUserInfoStorage userInfoDbStorage)
    {
        _accountDbStorage = accountDbStorage;
        _userInfoDbStorage = userInfoDbStorage;
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
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        var account = _accountDbStorage.GetAccount(loginViewModel.Email);
        if (account == null)
        {
            ModelState.AddModelError("", "There is no such account");
            return View(loginViewModel);
        }

        if (loginViewModel.Password != account.Password)
        {
            ModelState.AddModelError("", "Your password is incorrect");
        }

        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }

        return RedirectToAction("Details");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(AccountViewModel accountViewModel)
    {
        var accountById = _accountDbStorage.GetAccountById(Guid.Parse(GetUserId()));
        if (accountById == null)
        {
            accountViewModel.Id = Guid.Parse(GetUserId());
        }
        else
        {
            accountViewModel.Id = Guid.NewGuid();
        }

        var accountByEmail = _accountDbStorage.GetAccount(accountViewModel.Email);
        if (accountByEmail != null)
        {
            ModelState.AddModelError("", "Such account already exists");
        }

        if (accountViewModel.Email == accountViewModel.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (!ModelState.IsValid)
        {
            return View(accountViewModel);
        }

        _accountDbStorage.AddToList(Mapping<Account, AccountViewModel>.ToViewModel(accountViewModel));
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

    public IActionResult YandexLogin()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/Account/AppAdd" }, "Yandex");
    }

    public IActionResult VkontakteLogin()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/Account/AppAdd" }, "Vkontakte");
    }

    public IActionResult AppAdd()
    {
        var userId = Guid.Parse(GetUserId());
        var email = AppLogin.Email;
        var password = Guid.NewGuid().ToString().Substring(1, 7);
        
        var accountById = _accountDbStorage.GetAccountById(userId);
        if (accountById != null)
        {
            userId = Guid.NewGuid();
        }
        
        var accountByEmail = _accountDbStorage.GetAccount(email);
        if (accountByEmail == null)
        {
            _accountDbStorage.AddToList(new Account
            {
                Id = userId,
                Email = email,
                Password = password,
                ConfirmPassword = password,
                RoleName = "User",
                Picture = AppLogin.Picture
            });
            _userInfoDbStorage.AddToList(new UserInfo
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
        }

        return RedirectToAction("Details");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}