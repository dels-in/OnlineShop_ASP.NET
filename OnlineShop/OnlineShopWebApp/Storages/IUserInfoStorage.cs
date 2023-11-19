using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Storages;

public interface IUserInfoStorage
{
    void AddToList(UserInfoViewModel parameter);
    UserInfoViewModel GetUserInfo(Guid userId);
    void ChangeUserInfo(UserInfoViewModel account);
}