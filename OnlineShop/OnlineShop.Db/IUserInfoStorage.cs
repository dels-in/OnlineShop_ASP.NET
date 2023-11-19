using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public interface IUserInfoStorage
{
    void AddToList(UserInfo userInfo);
    UserInfo GetUserInfo(Guid userId);
    void ChangeUserInfo(UserInfo userInfo);
}