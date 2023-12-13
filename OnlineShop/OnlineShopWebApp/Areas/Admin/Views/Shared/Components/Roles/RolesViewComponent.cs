using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        return View("Roles", roles);
    }
}