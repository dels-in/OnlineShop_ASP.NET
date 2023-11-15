using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Storages;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly IAccountStorage _inMemoryAccountStorage;
    private readonly IUserInfoStorage _inMemoryUserInfoStorage;

    public UsersController(IAccountStorage inMemoryAccountStorage, IUserInfoStorage inMemoryUserInfoStorage)
    {
        _inMemoryAccountStorage = inMemoryAccountStorage;
        _inMemoryUserInfoStorage = inMemoryUserInfoStorage;
    }

    public IActionResult Index()
    {
        return View(_inMemoryAccountStorage.GetAll());
    }

    public IActionResult Details(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Account account)
    {
        if (_inMemoryAccountStorage.GetAccount(account.Email) != null)
        {
            ModelState.AddModelError("", "Such user already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.AddToList(account);
        return RedirectToAction("Index");
    }

    public IActionResult ChangePassword(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }

    [HttpPost]
    public IActionResult ChangePassword(Account account)
    {
        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.ChangePassword(account);
        return RedirectToAction("Details", new { userId = account.UserId });
    }

    public IActionResult ChangeUserInfo(Guid userId)
    {
        return View(_inMemoryUserInfoStorage.GetUserInfo(userId));
    }

    [HttpPost]
    public IActionResult ChangeUserInfo(UserInfoViewModel userInfoViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userInfoViewModel);
        }

        _inMemoryUserInfoStorage.ChangeUserInfo(userInfoViewModel);
        return RedirectToAction("Details", new { userId = userInfoViewModel.UserId });
    }


    public IActionResult ChangeUserRole(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }

    [HttpPost]
    public IActionResult ChangeUserRole(Account account)
    {
        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.ChangeRole(account);
        return RedirectToAction("Details", new { userId = account.UserId });
    }


    public IActionResult Delete(Guid userId)
    {
        _inMemoryAccountStorage.Delete(userId);
        return RedirectToAction("Index");
    }
}