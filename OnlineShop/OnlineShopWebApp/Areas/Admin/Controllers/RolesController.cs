using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        return View(_roleManager.Roles.ToList());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(IdentityRole role)
    {
        if (_roleManager.FindByNameAsync(role.Name).Result != null)
        {
            ModelState.AddModelError("", "Such role already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(role);
        }

        _roleManager.CreateAsync(role).Wait();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(string oldRoleName, string newRoleName)
    {
        var oldRole = _roleManager.FindByNameAsync(oldRoleName).Result;
        oldRole.Name = newRoleName;
        _roleManager.UpdateAsync(oldRole).Wait();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(string roleName)
    {
        var role = _roleManager.FindByNameAsync(roleName).Result;
        _roleManager.DeleteAsync(role).Wait();
        return RedirectToAction("Index");
    }
}