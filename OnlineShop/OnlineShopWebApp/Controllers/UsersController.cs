using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers;

public class UsersController : Controller
{
    private readonly IUserInfoStorage _userInfoDbStorage;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UsersController(IUserInfoStorage userInfoDbStorage,
        UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userInfoDbStorage = userInfoDbStorage;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Details()
    {
        return View("Details");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserViewModel userViewModel)
    {
        var accountByEmail = _userManager.FindByNameAsync(userViewModel.Email).Result;

        if (accountByEmail != null)
        {
            ModelState.AddModelError("", "Such account already exists");
        }

        if (userViewModel.Email == userViewModel.Password)
        {
            ModelState.AddModelError("", "Email and password must not match");
        }

        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        userViewModel.Password = userViewModel.Password.Encrypt();
        userViewModel.ConfirmPassword = userViewModel.Password;

        var user = Mapping<User, UserViewModel>.ToViewModel(userViewModel);
        user.UserName = userViewModel.Email;

        var result = _userManager
            .CreateAsync(user, userViewModel.Password).Result;

        if (result.Succeeded)
        {
            return RedirectToAction("Details");
        }

        return View(userViewModel);
    }

    public IActionResult Login(string returnUrl)
    {
        returnUrl ??= "Details";
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        var accountByEmail = _userManager.FindByNameAsync(loginViewModel.Email).Result;

        if (accountByEmail == null)
        {
            ModelState.AddModelError("", "Such account does not exist");
        }

        if (ModelState.IsValid)
        {
            var result = _signInManager
                .PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password.Encrypt(), loginViewModel.IsChecked,
                    false)
                .Result;
            if (result.Succeeded)
            {
                return Redirect(loginViewModel.ReturnUrl);
            }

            ModelState.AddModelError("", "Invalid password");
        }

        return View(loginViewModel);
    }

    public IActionResult GithubLogin(LoginViewModel loginViewModel)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/User/AppAdd?returnUrl={loginViewModel.ReturnUrl}" },
            "Github");
    }

    public IActionResult GoogleLogin(LoginViewModel loginViewModel)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/User/AppAdd?returnUrl={loginViewModel.ReturnUrl}" },
            "Google");
    }

    public IActionResult YandexLogin(LoginViewModel loginViewModel)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/User/AppAdd?returnUrl={loginViewModel.ReturnUrl}" },
            "Yandex");
    }

    public IActionResult VkontakteLogin(LoginViewModel loginViewModel)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/User/AppAdd?returnUrl={loginViewModel.ReturnUrl}" },
            "Vkontakte");
    }

    public IActionResult AppAdd(string returnUrl)
    {
        var email = AppLogin.Email;

        var password = Guid.NewGuid().ToString().Substring(1, 7).Encrypt();

        var userByEmail = _userManager.FindByNameAsync(email).Result;
        if (userByEmail == null)
        {
            _userManager.CreateAsync(
                new User
                {
                    UserName = email,
                    Email = email,
                    Password = password.Encrypt(),
                    ConfirmPassword = password.Encrypt(),
                    Picture = AppLogin.Picture
                },
                password);
            _userInfoDbStorage.AddToList(new UserInfo
            {
                UserId = Guid.Parse(_userManager.GetUserId(ClaimsPrincipal.Current)),
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

        var result = _signInManager
            .PasswordSignInAsync(email, password.Encrypt(), false, false)
            .Result;
        if (result.Succeeded)
        {
            return Redirect(returnUrl);
        }

        ModelState.AddModelError("", "Invalid password");
        return RedirectToAction("Login");
    }
}