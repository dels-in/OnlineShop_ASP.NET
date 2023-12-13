using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var accountByEmail = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == userViewModel.Email);

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
            var roles = new List<Role> { _roleManager.FindByNameAsync("User").Result };
            userViewModel.Roles = roles;

            var user = userViewModel.ToUser();

            var result = _userManager
                .CreateAsync(user, user.Password).Result;
            if (result.Succeeded)
            {
                var roleNames = roles.Select(role => role.Name).ToList();
                return SignIn(userViewModel.Email, userViewModel.IsChecked, userViewModel.ReturnUrl, roleNames);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userViewModel);
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
        var accountByEmail = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == loginViewModel.Email);

        if (accountByEmail == null)
        {
            ModelState.AddModelError("", "Such account does not exist");
        }

        if (ModelState.IsValid)
        {
            if (accountByEmail.Password.Decrypt() == loginViewModel.Password)
            {
                var roleNames = accountByEmail.Roles.Select(role => role.Name).ToList();
                return SignIn(accountByEmail.Email, loginViewModel.IsChecked, loginViewModel.ReturnUrl, roleNames);
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

        var userByEmail = _userManager.FindByNameAsync(email).Result;
        var roles = new List<Role> { _roleManager.FindByNameAsync("User").Result };
        if (userByEmail == null)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                Password = password.Encrypt(),
                ConfirmPassword = password.Encrypt(),
                Picture = AppLogin.Picture,
                Roles = roles,
            };

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

        var roleNames = roles.Select(role => role.Name).ToList();
        return SignIn(email, true, returnUrl, roleNames);
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync().Wait();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult SignIn(string email, bool isPersistent, string returnUrl, List<string> roleNames)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, email)
        };
        foreach (var name in roleNames)
        {
            claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, name));
        }

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