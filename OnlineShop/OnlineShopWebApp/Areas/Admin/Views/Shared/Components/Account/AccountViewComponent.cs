using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Account;

public class AccountViewComponent : ViewComponent
{
    private readonly UserManager<OnlineShop.Db.Models.User> _userManager;

    public AccountViewComponent(UserManager<OnlineShop.Db.Models.User> userManager)
    {
        _userManager = userManager;
    }

    public IViewComponentResult Invoke(string email)
    {
        var user = Mapping<UserViewModel, OnlineShop.Db.Models.User>.ToViewModel(_userManager.FindByNameAsync(email).Result);
        return View("Account", user);
    }
}