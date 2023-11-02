using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Areas.Admin.Controllers;

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

    public IActionResult Users()
    {
        return View(_inMemoryAccountStorage.GetAll());
    }

    public IActionResult UserDetails(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }

    public IActionResult AddNewUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewUser(Account account)
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
        return RedirectToAction("Users");
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
        return RedirectToAction("UserDetails", new { userId = account.UserId });
    }

    public IActionResult ChangeUserInfo(Guid userId)
    {
        return View(_inMemoryUserInfoStorage.GetUserInfo(userId));
    }

    [HttpPost]
    public IActionResult ChangeUserInfo(UserInfo userInfo)
    {
        if (!ModelState.IsValid)
        {
            return View(userInfo);
        }

        _inMemoryUserInfoStorage.ChangeUserInfo(userInfo);
        return RedirectToAction("UserDetails", new { userId = userInfo.UserId });
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
        return RedirectToAction("UserDetails", new { userId = account.UserId });
    }


    public IActionResult DeleteUser(Guid userId)
    {
        _inMemoryAccountStorage.Delete(userId);
        return RedirectToAction("Users");
    }
}