using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.UserInfo;

public class UserInfoViewComponent : ViewComponent
{
    private readonly IUserInfoStorage _userInfoDbStorage;

    public UserInfoViewComponent(IUserInfoStorage userInfoDbStorage)
    {
        _userInfoDbStorage = userInfoDbStorage;
    }

    public IViewComponentResult Invoke(Guid userId, string propertyName)
    {
        var userInfo = Mapping<UserInfoViewModel, OnlineShop.Db.Models.UserInfo>.ToViewModel(_userInfoDbStorage.GetUserInfo(userId));
        if (userInfo == null)
            return View("Default");
        switch (propertyName)
        {
            case "FirstName":
                return View("UserInfoFirstName", userInfo);
            case "LastName":
                return View("UserInfoLastName", userInfo);
            case "Address":
                return View("UserInfoAddress", userInfo);
            default:
                return View("Default");
        }
    }
}