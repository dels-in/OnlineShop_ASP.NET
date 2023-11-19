using OnlineShopWebApp.Areas.Admin.Controllers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Storages;

public class InMemoryUserInfoStorage : IUserInfoStorage
{
    private readonly IFileStorage _inMemoryFileStorage;
    private readonly IAccountStorage _inMemoryAccountStorage;
    private readonly List<UserInfoViewModel> _usersInfo;

    public InMemoryUserInfoStorage(IFileStorage inMemoryFileStorage, IAccountStorage inMemoryAccountStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _inMemoryAccountStorage = inMemoryAccountStorage;
        _usersInfo = _inMemoryFileStorage.Load<UserInfoViewModel>("UsersInfo.json");
    }

    public void AddToList(UserInfoViewModel userInfoViewModel)
    {
        _usersInfo.Add(userInfoViewModel);
        _inMemoryFileStorage.Save(_usersInfo, "UsersInfo.json");
    }

    public UserInfoViewModel GetUserInfo(Guid userId)
    {
        var ui = _usersInfo.FirstOrDefault(userInfo => userInfo.UserId == userId);
        if (ui == null)
            ui = new UserInfoViewModel
            {
                UserId = userId, Email = _inMemoryAccountStorage.GetAccountById(userId).Email, FirstName = null,
                LastName = null, Address = null, Address2 = null,
                City = null, Region = null, PostCode = null, IsChecked = true
            };
        return ui;
    }

    public void ChangeUserInfo(UserInfoViewModel userInfoViewModel)
    {
        var userInfoToChange = GetUserInfo((Guid)userInfoViewModel.UserId);
        if (userInfoToChange == null) return;
        userInfoToChange.FirstName = userInfoViewModel.FirstName;
        userInfoToChange.LastName = userInfoViewModel.LastName;
        userInfoToChange.Address = userInfoViewModel.Address;
        userInfoToChange.Address2 = userInfoViewModel.Address2;
        userInfoToChange.City = userInfoViewModel.City;
        userInfoToChange.Region = userInfoViewModel.Region;
        userInfoToChange.PostCode = userInfoViewModel.PostCode;
        _usersInfo.Remove(GetUserInfo((Guid)userInfoViewModel.UserId));
        _usersInfo.Add(userInfoToChange);
    }
}