using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AccountsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUserInfoStorage _userInfoDbStorage;
    private readonly RoleManager<Role> _roleManager;

    public AccountsController(IUserInfoStorage userInfoDbStorage, UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _userInfoDbStorage = userInfoDbStorage;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.Include(u => u.Roles).ToList();
        return View(users.ToUserViewModelList());
    }

    public IActionResult Details(string email)
    {
        var user = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == email);
        return View(user.ToUserViewModel());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(UserViewModel userViewModel, List<string> roleNames)
    {
        if (_userManager.FindByNameAsync(userViewModel.Email).Result != null)
        {
            ModelState.AddModelError("", "Such user already exists");
        }

        if (ModelState.IsValid)
        {
            var roles = new List<Role>();
            foreach (var name in roleNames)
            {
                roles.Add(_roleManager.FindByNameAsync(name).Result);
            }
            userViewModel.Roles = roles;
            var result = _userManager.CreateAsync(userViewModel.ToUser(), userViewModel.Password.Encrypt()).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userViewModel);
        }

        return View(userViewModel);
    }

    public IActionResult ChangePassword(string email)
    {
        var user = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == email);
        return View(user.ToUserViewModel());
    }

    [HttpPost]
    public IActionResult ChangePassword(UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.FindByNameAsync(userViewModel.Email).Result;
            user.Password = userViewModel.Password.Encrypt();
            user.ConfirmPassword = user.Password;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.Password);

            _userManager.UpdateAsync(user).Wait();
            return RedirectToAction("Details", new { email = userViewModel.Email });
        }

        return View(userViewModel);
    }

    public IActionResult ChangeUserInfo(Guid userId)
    {
        return View(Mapping<UserInfoViewModel, UserInfo>.ToViewModel(_userInfoDbStorage.GetUserInfo(userId)));
    }

    [HttpPost]
    public IActionResult ChangeUserInfo(UserInfoViewModel userInfoViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userInfoViewModel);
        }

        _userInfoDbStorage.ChangeUserInfo(Mapping<UserInfo, UserInfoViewModel>.ToViewModel(userInfoViewModel));
        return RedirectToAction("Details", new { email = userInfoViewModel.Email });
    }


    public IActionResult ChangeUserRole(string email)
    {
        var user = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == email);
        return View(user.ToUserViewModel());
    }

    [HttpPost]
    public IActionResult ChangeUserRole(UserViewModel userViewModel, List<string> roleNames)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == userViewModel.Email);
            user.Roles.Clear();
            foreach (var name in roleNames)
            {
                user.Roles.Add(_roleManager.FindByNameAsync(name).Result);
            }

            _userManager.UpdateAsync(user).Wait();
            return RedirectToAction("Details", new { email = userViewModel.Email });
        }

        return View(userViewModel);
    }


    public IActionResult Delete(string email)
    {
        var userToDelete = _userManager.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == email);
        var currentUser = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
        if (currentUser != userToDelete)
            _userManager.DeleteAsync(userToDelete).Wait();
        return RedirectToAction("Index");
    }
}