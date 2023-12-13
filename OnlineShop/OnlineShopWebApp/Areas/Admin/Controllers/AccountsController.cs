using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public AccountsController(IUserInfoStorage userInfoDbStorage, UserManager<User> userManager)
    {
        _userInfoDbStorage = userInfoDbStorage;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users.ToUserViewModelList());
    }

    public IActionResult Details(string email)
    {
        return View(_userManager.FindByNameAsync(email).Result.ToUserViewModel());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(UserViewModel userViewModel)
    {
        if (_userManager.FindByNameAsync(userViewModel.Email).Result != null)
        {
            ModelState.AddModelError("", "Such user already exists");
        }

        if (ModelState.IsValid)
        {
            var result = _userManager.CreateAsync(userViewModel.ToUser(), userViewModel.Password.Encrypt()).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
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

    public IActionResult ChangePassword(string email)
    {
        return View(_userManager.FindByNameAsync(email).Result.ToUserViewModel());
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
        return View(_userManager.FindByNameAsync(email).Result.ToUserViewModel());
    }

    [HttpPost]
    public IActionResult ChangeUserRole(UserViewModel userViewModel, string roleName)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.FindByNameAsync(userViewModel.Email).Result;
            user.RoleName = roleName;

            _userManager.UpdateAsync(user).Wait();
            return RedirectToAction("Details", new { email = userViewModel.Email });
        }

        return View(userViewModel);
    }


    public IActionResult Delete(string email)
    {
        var userToDelete = _userManager.FindByNameAsync(email).Result;
        var currentUser = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
        if (currentUser != userToDelete)
            _userManager.DeleteAsync(userToDelete).Wait();
        return RedirectToAction("Index");
    }
}