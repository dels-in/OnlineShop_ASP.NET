using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Areas.Admin.Views.Shared.Components.Roles;

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