using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class RolesController : Controller
{
    private readonly IRoleStorage _rolesDbStorage;
    
    public RolesController(IRoleStorage rolesDbStorage)
    {
        _rolesDbStorage = rolesDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<RoleViewModel, Role>.ToViewModelList(_rolesDbStorage.GetAll()));
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(RoleViewModel roleViewModel)
    {
        if (_rolesDbStorage.GetRole(roleViewModel.RoleName) != null)
        {
            ModelState.AddModelError("", "Such role already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(roleViewModel);
        }

        _rolesDbStorage.Add(Mapping<Role, RoleViewModel>.ToViewModel(roleViewModel));
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(string oldRoleName, string newRoleName)
    {
        _rolesDbStorage.Edit(oldRoleName, newRoleName);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(string roleName)
    {
        _rolesDbStorage.Delete(roleName);
        return RedirectToAction("Index");
    }
}