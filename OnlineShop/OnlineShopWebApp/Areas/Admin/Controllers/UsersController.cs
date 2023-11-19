using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly IAccountStorage _accountDbStorage;
    private readonly IUserInfoStorage _userInfoDbStorage;

    public UsersController(IAccountStorage accountDbStorage, IUserInfoStorage userInfoDbStorage)
    {
        _accountDbStorage = accountDbStorage;
        _userInfoDbStorage = userInfoDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<AccountViewModel, Account>.ToViewModelList(_accountDbStorage.GetAll()));
    }

    public IActionResult Details(Guid userId)
    {
        return View(Mapping<AccountViewModel, Account>.ToViewModel(_accountDbStorage.GetAccountById(userId)));
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(AccountViewModel accountViewModel)
    {
        if (_accountDbStorage.GetAccount(accountViewModel.Email) != null)
        {
            ModelState.AddModelError("", "Such user already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(accountViewModel);
        }

        _accountDbStorage.AddToList(Mapping<Account, AccountViewModel>.ToViewModel(accountViewModel));
        return RedirectToAction("Index");
    }

    public IActionResult ChangePassword(Guid userId)
    {
        return View(Mapping<AccountViewModel, Account>.ToViewModel(_accountDbStorage.GetAccountById(userId)));
    }

    [HttpPost]
    public IActionResult ChangePassword(AccountViewModel accountViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(accountViewModel);
        }

        _accountDbStorage.ChangePassword(Mapping<Account, AccountViewModel>.ToViewModel(accountViewModel));
        return RedirectToAction("Details", new { userId = accountViewModel.Id });
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


    public IActionResult ChangeUserRole(Guid userId)
    {
        return View(Mapping<AccountViewModel, Account>.ToViewModel(_accountDbStorage.GetAccountById(userId)));
    }

    [HttpPost]
    public IActionResult ChangeUserRole(AccountViewModel accountViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(accountViewModel);
        }

        _accountDbStorage.ChangeRole(Mapping<Account, AccountViewModel>.ToViewModel(accountViewModel));
        return RedirectToAction("Details", new { userId = accountViewModel.Id });
    }


    public IActionResult Delete(Guid userId)
    {
        _accountDbStorage.Delete(userId);
        return RedirectToAction("Index");
    }
}