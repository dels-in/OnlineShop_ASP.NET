using WebApplication1.Areas.Admin.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryUserInfoStorage : IUserInfoStorage
{
    private readonly IFileStorage _inMemoryFileStorage;
    private readonly IAccountStorage _inMemoryAccountStorage;
    private readonly List<UserInfo> _usersInfo;

    public InMemoryUserInfoStorage(IFileStorage inMemoryFileStorage,  IAccountStorage inMemoryAccountStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _inMemoryAccountStorage = inMemoryAccountStorage;
        _usersInfo = _inMemoryFileStorage.Load<UserInfo>("UsersInfo.json");
    }

    public void AddToList(UserInfo userInfo)
    {
        _usersInfo.Add(userInfo);
        _inMemoryFileStorage.Save(_usersInfo, "UsersInfo.json");
    }

    public List<UserInfo> GetAll()
    {
        return _usersInfo;
    }

    public UserInfo GetUserInfo(Guid userId)
    {
        var ui = _usersInfo.FirstOrDefault(userInfo => userInfo.UserId == userId);
        if (ui == null)
            ui = new UserInfo
            {
                UserId = userId, Email = _inMemoryAccountStorage.GetAccountById(userId).Email, FirstName = null, LastName = null, Address = null, Address2 = null,
                City = null, Region = null, PostCode = null, IsChecked = true
            };
        return ui;
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
        _usersInfo.Remove(GetUserInfo(userInfo.UserId));
        _usersInfo.Add(userInfoToChange);
    }
}