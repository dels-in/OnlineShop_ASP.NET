using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Storages;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles;

public class RolesViewComponent : ViewComponent
{
    private readonly IRoleStorage _inMemoryRoleStorage;

    public RolesViewComponent(IRoleStorage inMemoryRoleStorage)
    {
        _inMemoryRoleStorage = inMemoryRoleStorage;
    }

    public IViewComponentResult Invoke()
    {
        return View("Roles", _inMemoryRoleStorage.GetAll());
    }
}