using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.User;

public class UserViewComponent : ViewComponent
{
    private readonly UserManager<OnlineShop.Db.Models.User> _userManager;

    public UserViewComponent(UserManager<OnlineShop.Db.Models.User> userManager)
    {
        _userManager = userManager;
    }

    public IViewComponentResult Invoke()
    {
        var user = _userManager.GetUserAsync((ClaimsPrincipal)User).Result.ToUserViewModel();
        return View("User", user);
    }
}