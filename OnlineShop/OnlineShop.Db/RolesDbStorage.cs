using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class RolesDbStorage : IRoleStorage
{
    private readonly DatabaseContext _dbContext;

    public RolesDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Role> GetAll()
    {
        return _dbContext.Roles.ToList();
    }

    public Role GetRole(string roleName)
    {
        return _dbContext.Roles.FirstOrDefault(r => r.RoleName.ToUpper() == roleName.ToUpper());
    }

    public void Add(Role role)
    {
        _dbContext.Roles.Add(role);
        _dbContext.SaveChanges();
    }

    public void Edit(string oldRoleName, string newRoleName)
    {
        var role = GetRole(oldRoleName);
        role.RoleName = newRoleName;
        _dbContext.SaveChanges();
    }

    public void Delete(string roleName)
    {
        _dbContext.Roles.Remove(GetRole(roleName));
        _dbContext.SaveChanges();
    }

    private void AddToList()
    {
        _dbContext.Roles.Add(new Role{RoleName = "Admin"});
        _dbContext.Roles.Add(new Role{RoleName ="User"});
    }
}