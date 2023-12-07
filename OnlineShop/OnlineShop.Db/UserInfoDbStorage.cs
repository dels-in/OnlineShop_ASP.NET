using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class UserInfoDbStorage : IUserInfoStorage
{
    private readonly UserManager<User> _userManager;
    private readonly DatabaseContext _dbContext;

    public UserInfoDbStorage(DatabaseContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public void AddToList(UserInfo userInfo)
    {
        _dbContext.UserInfos.Add(userInfo);
        _dbContext.SaveChanges();
    }

    public UserInfo GetUserInfo(Guid userId)
    {
        var userInfo = _dbContext.UserInfos.FirstOrDefault(userInfo => userInfo.UserId == userId);
        if (userInfo == null)
        {
            userInfo = new UserInfo
            {
                UserId = userId,
                Email = _userManager.GetEmailAsync(_userManager.GetUserAsync(ClaimsPrincipal.Current).Result).Result,
                FirstName = null,
                LastName = null, Address = null, Address2 = null,
                City = null, Region = null, PostCode = null, IsChecked = true
            };
            AddToList(userInfo);
        }

        return userInfo;
    }

    public void ChangeUserInfo(UserInfo userInfo)
    {
        var userInfoToChange = GetUserInfo(userInfo.UserId);
        if (userInfoToChange == null) return;
        userInfoToChange.FirstName = userInfo.FirstName;
        userInfoToChange.LastName = userInfo.LastName;
        userInfoToChange.Address = userInfo.Address;
        userInfoToChange.Address2 = userInfo.Address2;
        userInfoToChange.City = userInfo.City;
        userInfoToChange.Region = userInfo.Region;
        userInfoToChange.PostCode = userInfo.PostCode;
        userInfoToChange.IsChecked = userInfo.IsChecked;

        _dbContext.UserInfos.Remove(GetUserInfo(userInfo.UserId));
         _dbContext.SaveChanges();

        _dbContext.UserInfos.Add(userInfoToChange);
        _dbContext.SaveChanges();
    }
}