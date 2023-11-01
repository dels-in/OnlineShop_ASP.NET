using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers;

public interface IUserInfoStorage
{
    void AddToList(UserInfo parameter);
    List<UserInfo> GetAll();
    UserInfo GetUserInfo(Guid userId);
    void ChangeUserInfo(UserInfo account);
}