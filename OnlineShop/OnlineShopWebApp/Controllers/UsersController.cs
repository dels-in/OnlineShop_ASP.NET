using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    private readonly RoleManager<Role> _roleManager;

    public UsersController(IUserInfoStorage userInfoDbStorage, UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _userInfoDbStorage = userInfoDbStorage;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Details()
    {
        return View("Details");
    }

    public IActionResult Register(string returnUrl)
    {
        return View(new UserViewModel { ReturnUrl = returnUrl });
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

        if (ModelState.IsValid)
        {
            userViewModel.RoleName = "User";

            var user = userViewModel.ToUser();

            var result = _userManager
                .CreateAsync(user, user.Password).Result;
            if (result.Succeeded)
            {
                SignIn(userViewModel.Email, userViewModel.RoleName,
                    userViewModel.IsChecked, userViewModel.ReturnUrl);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(userViewModel);
            }
        }

        return View(userViewModel);
    }

    public IActionResult Login(string returnUrl)
    {
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
            if (accountByEmail.Password.Decrypt() == loginViewModel.Password)
            {
                return SignIn(accountByEmail.Email, accountByEmail.RoleName, loginViewModel.IsChecked,
                    loginViewModel.ReturnUrl);
            }

            ModelState.AddModelError("", "Invalid password");
        }

        return View(loginViewModel);
    }

    public IActionResult GithubLogin(string returnUrl)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/Users/AppAdd?returnUrl={returnUrl}" },
            "Github");
    }

    public IActionResult GoogleLogin(string returnUrl)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/Users/AppAdd?returnUrl={returnUrl}" },
            "Google");
    }

    public IActionResult YandexLogin(string returnUrl)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/Users/AppAdd?returnUrl={returnUrl}" },
            "Yandex");
    }

    public IActionResult VkontakteLogin(string returnUrl)
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = $"/Users/AppAdd?returnUrl={returnUrl}" },
            "Vkontakte");
    }

    public IActionResult AppAdd(string returnUrl)
    {
        var email = AppLogin.Email;
        var password = Guid.NewGuid().ToString().Substring(1, 7).Encrypt();
        var user = new User
        {
            UserName = email,
            Email = email,
            Password = password.Encrypt(),
            ConfirmPassword = password.Encrypt(),
            Picture = AppLogin.Picture,
            RoleName = _roleManager.FindByNameAsync("User").Result.Id,
        };

        var userByEmail = _userManager.FindByNameAsync(email).Result;
        if (userByEmail == null)
        {
            _userManager.CreateAsync(user, password.Encrypt()).Wait();
            _userInfoDbStorage.AddToList(new UserInfo
            {
                UserId = Guid.Parse(_userManager.GetUserIdAsync(user).Result),
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

        return SignIn(email, "User", true, returnUrl);
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync().Wait();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult SignIn(string email, string roleName, bool isPersistent, string returnUrl)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, email),
            new(ClaimsIdentity.DefaultRoleClaimType, roleName)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            }).Wait();
        return Redirect(returnUrl ?? "~/Users/Details");
    }
}