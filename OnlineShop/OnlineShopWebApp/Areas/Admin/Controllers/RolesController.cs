using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<Role> _roleManager;

    public RolesController(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var rolesList = _roleManager.Roles.ToList();
        return View(rolesList.ToRoleViewModelList());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(RoleViewModel roleViewModel)
    {
        if (_roleManager.FindByNameAsync(roleViewModel.Name).Result != null)
        {
            ModelState.AddModelError("", "Such role already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(roleViewModel);
        }

        var result = _roleManager.CreateAsync(roleViewModel.ToRole()).Result;
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(roleViewModel);
        }
    }

    [HttpPost]
    public IActionResult Edit(string oldRoleName, string newRoleName)
    {
        var oldRole = _roleManager.FindByNameAsync(oldRoleName).Result;
        if (oldRole != null)
        {
            oldRole.Name = newRoleName;
            _roleManager.UpdateAsync(oldRole).Wait();
        }

        return RedirectToAction("Index");
    }

    public IActionResult Delete(string roleName)
    {
        var role = _roleManager.FindByNameAsync(roleName).Result;
        if (role != null)
        {
            _roleManager.DeleteAsync(role).Wait();
        }

        return RedirectToAction("Index");
    }
}