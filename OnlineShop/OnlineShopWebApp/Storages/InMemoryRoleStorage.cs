using WebApplication1.Models;

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

    public Role GetRole(int roleId)
    {
        return _roles.FirstOrDefault(r => r.RoleId == roleId);
    }

    public void Add(Role role)
    {
        _roles.Add(role);
    }

    public void Edit(int roleId, string roleName)
    {
        var role = GetRole(roleId);
        role.RoleName = roleName;
    }

    public void Delete(int roleId)
    {
        _roles.Remove(GetRole(roleId));
        _inMemoryFileStorage.Save(_roles, "Roles.json");
    }

    private void AddToList()
    {
        _roles.Add(new Role("Admin"));
        _roles.Add(new Role("User"));
        _inMemoryFileStorage.Save(_roles, "Roles.json");
    }
}