using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public interface IRoleStorage
{
    List<Role> GetAll();
    Role GetRole(string roleName);
    void Add(Role role);
    void Delete(string roleName);
    void Edit(string oldRoleName, string newRoleName);
}