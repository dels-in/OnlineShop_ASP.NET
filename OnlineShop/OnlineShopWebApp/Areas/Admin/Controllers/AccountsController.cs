using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
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

        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        _userManager.CreateAsync(userViewModel.ToUser(), userViewModel.Password);
        return RedirectToAction("Index");
    }

    public IActionResult ChangePassword(string email)
    {
        return View(_userManager.FindByNameAsync(email).Result.ToUserViewModel());
    }

    [HttpPost]
    public IActionResult ChangePassword(UserViewModel userViewModel, string oldPassword)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        _userManager.ChangePasswordAsync(userViewModel.ToUser(), oldPassword,
            userViewModel.Password);
        return RedirectToAction("Details", new { email = userViewModel.Email });
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
        return RedirectToAction("Details", new { userId = userInfoViewModel.UserId });
    }


    public IActionResult ChangeUserRole(string email)
    {
        return View(Mapping<UserViewModel, User>.ToViewModel(_userManager.FindByNameAsync(email).Result));
    }

    [HttpPost]
    public IActionResult ChangeUserRole(UserViewModel userViewModel, string roleName)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        _userManager.AddToRoleAsync(Mapping<User, UserViewModel>.ToViewModel(userViewModel), roleName).Wait();
        
        return RedirectToAction("Details", new { userId = userViewModel.Id });
    }


    public IActionResult Delete(string email)
    {
        _userManager.DeleteAsync(_userManager.FindByNameAsync(email).Result);
        return RedirectToAction("Index");
    }
}