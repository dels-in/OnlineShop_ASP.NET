using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class UserInfoDbStorage : IUserInfoStorage
{
    private readonly DatabaseContext _dbContext;

    public UserInfoDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void AddToList(UserInfo userInfo)
    {
        _dbContext.UserInfo.Add(userInfo);
        _dbContext.SaveChanges();
    }

    public UserInfo GetUserInfo(Guid userId)
    {
        var ui = _dbContext.UserInfo.FirstOrDefault(userInfo => userInfo.UserId == userId) ?? new UserInfo
        {
            UserId = userId, Email = _dbContext.Accounts.GetAccountById(userId).Email, FirstName = null,
            LastName = null, Address = null, Address2 = null,
            City = null, Region = null, PostCode = null, IsChecked = true
        };
        _dbContext.SaveChanges();

        return ui;
    }

    public void ChangeUserInfo(UserInfo userInfo)
    {
        var userInfoToChange = GetUserInfo((Guid)userInfo.UserId);
        if (userInfoToChange == null) return;
        userInfoToChange.FirstName = userInfo.FirstName;
        userInfoToChange.LastName = userInfo.LastName;
        userInfoToChange.Address = userInfo.Address;
        userInfoToChange.Address2 = userInfo.Address2;
        userInfoToChange.City = userInfo.City;
        userInfoToChange.Region = userInfo.Region;
        userInfoToChange.PostCode = userInfo.PostCode;

        _dbContext.UserInfo.Remove(GetUserInfo((Guid)userInfo.UserId));
        _dbContext.UserInfo.Add(userInfoToChange);

        _dbContext.SaveChanges();
    }
}