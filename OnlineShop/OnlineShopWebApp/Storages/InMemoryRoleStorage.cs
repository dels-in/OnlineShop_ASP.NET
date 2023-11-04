using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Storages;

public class InMemoryRoleStorage : IRoleStorage
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<Role> _roles;

    public InMemoryRoleStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _roles = _inMemoryFileStorage.Load<Role>("Roles.json");
    }

    public List<Role> GetAll()
    {
        if (_roles.Count == 0)
            AddToList();
        return _roles;
    }

    public Role GetRole(string roleName)
    {
        return _roles.FirstOrDefault(r => r.RoleName.ToUpper() == roleName.ToUpper());
    }

    public void Add(Role role)
    {
        _roles.Add(role);
    }

    public void Edit(string oldRoleName, string newRoleName)
    {
        var role = GetRole(oldRoleName);
        role.RoleName = newRoleName;
    }

    public void Delete(string roleName)
    {
        _roles.Remove(GetRole(roleName));
        _inMemoryFileStorage.Save(_roles, "Roles.json");
    }

    private void AddToList()
    {
        _roles.Add(new Role{RoleName = "Admin"});
        _roles.Add(new Role{RoleName ="User"});
        _inMemoryFileStorage.Save(_roles, "Roles.json");
    }
}