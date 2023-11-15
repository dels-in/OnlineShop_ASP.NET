using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Controllers;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.UserInfo;

public class UserInfoViewComponent : ViewComponent
{
    private readonly IUserInfoStorage _inMemoryUserInfoStorage;

    public UserInfoViewComponent(IUserInfoStorage inMemoryUserInfoStorage)
    {
        _inMemoryUserInfoStorage = inMemoryUserInfoStorage;
    }

    public IViewComponentResult Invoke(Guid userId, string propertyName)
    {
        var userInfo = _inMemoryUserInfoStorage.GetUserInfo(userId);
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