using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

public interface IUserInfoStorage
{
    void AddToList(UserInfoViewModel parameter);
    List<UserInfoViewModel> GetAll();
    UserInfoViewModel GetUserInfo(Guid userId);
    void ChangeUserInfo(UserInfoViewModel account);
}