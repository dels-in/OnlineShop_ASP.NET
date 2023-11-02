using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Storages;

namespace WebApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class RolesController : Controller
{
    private readonly IRoleStorage _inMemoryRoleStorage;
    
    public RolesController(IRoleStorage inMemoryRoleStorage)
    {
        _inMemoryRoleStorage = inMemoryRoleStorage;
    }

    public IActionResult Roles()
    {
        return View(_inMemoryRoleStorage.GetAll());
    }

    public IActionResult AddNewRole()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewRole(Role role)
    {
        if (_inMemoryRoleStorage.GetRole(role.RoleName) != null)
        {
            ModelState.AddModelError("", "Such role already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(role);
        }

        _inMemoryRoleStorage.Add(role);
        return RedirectToAction("Roles");
    }

    [HttpPost]
    public IActionResult EditRole(string oldRoleName, string newRoleName)
    {
        _inMemoryRoleStorage.Edit(oldRoleName, newRoleName);
        return RedirectToAction("Roles");
    }

    public IActionResult DeleteRole(string roleName)
    {
        _inMemoryRoleStorage.Delete(roleName);
        return RedirectToAction("Roles");
    }
}