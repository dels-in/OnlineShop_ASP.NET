using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles;

public class RolesViewComponent : ViewComponent
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesViewComponent(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IViewComponentResult Invoke()
    {
        var roles = _roleManager.Roles.ToList();
        return View("Roles", roles);
    }
}