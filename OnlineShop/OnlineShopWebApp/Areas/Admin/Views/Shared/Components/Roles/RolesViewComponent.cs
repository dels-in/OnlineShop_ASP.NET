using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Storages;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Roles;

public class RolesViewComponent : ViewComponent
{
    private readonly IRoleStorage _rolesDbStorage;

    public RolesViewComponent(IRoleStorage rolesDbStorage)
    {
        _rolesDbStorage = rolesDbStorage;
    }

    public IViewComponentResult Invoke()
    {
        return View("Roles", Mapping<RoleViewModel, Role>.ToViewModelList(_rolesDbStorage.GetAll()));
    }
}