using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles;

public class RolesViewComponent : ViewComponent
{
    private readonly RoleManager<Role> _roleManager;

    public RolesViewComponent(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public IViewComponentResult Invoke(List<Role> userRoles)
    {
        var roles = _roleManager.Roles.ToList();
        if (userRoles != null)
        {
            foreach (var role in userRoles)
            {
                roles.RemoveAll(r => r.Name == role.Name);
            }
        }

        return View("Roles", roles);
    }
}