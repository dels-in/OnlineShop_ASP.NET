using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IRoleStorage
{
    List<Role> GetAll();
    Role GetRole(int roleId);
    void Add(Role role);
    void Delete(int roleId);
    void Edit(int roleId, string roleName);
   
}