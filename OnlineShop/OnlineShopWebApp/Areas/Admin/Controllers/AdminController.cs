using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;
    private readonly IStorage<Order, UserInfo> _inMemoryCheckoutStorage;
    private readonly IRoleStorage _inMemoryRoleStorage;
    private readonly IAccountStorage _inMemoryAccountStorage;
    private readonly IUserInfoStorage _inMemoryUserInfoStorage;

    public AdminController(IProductStorage inMemoryProductStorage,
        IStorage<Order, UserInfo> inMemoryCheckoutStorage, IRoleStorage inMemoryRoleStorage,
        IAccountStorage inMemoryAccountStorage, IUserInfoStorage inMemoryUserInfoStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
        _inMemoryCheckoutStorage = inMemoryCheckoutStorage;
        _inMemoryRoleStorage = inMemoryRoleStorage;
        _inMemoryAccountStorage = inMemoryAccountStorage;
        _inMemoryUserInfoStorage = inMemoryUserInfoStorage;
    }

    public IActionResult Orders()
    {
        return View(_inMemoryCheckoutStorage.GetAll());
    }

    public IActionResult EditOrder(Guid orderId)
    {
        var order = _inMemoryCheckoutStorage.GetAll().FirstOrDefault(o => o.OrderId == orderId);
        return View(order);
    }

    [HttpPost]
    public IActionResult EditOrder(Guid orderId, OrderStatus orderStatus)
    {
        _inMemoryCheckoutStorage.Edit(orderId, orderStatus);
        return RedirectToAction("Orders");
    }

    public IActionResult Users()
    {
        return View(_inMemoryAccountStorage.GetAll());
    }
    
    public IActionResult UserDetails(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }

    public IActionResult AddNewUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewUser(Account account)
    {
        if (_inMemoryAccountStorage.GetAccount(account.Email) != null)
        {
            ModelState.AddModelError("", "Such user already exists");
        }

        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.AddToList(account);
        return RedirectToAction("Users");
    }
    
    public IActionResult ChangePassword(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }

    [HttpPost]
    public IActionResult ChangePassword(Account account)
    {
        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.ChangePassword(account);
        return RedirectToAction("UserDetails", new { userId = account.UserId });
    }

    public IActionResult ChangeUserInfo(Guid userId)
    {
        return View(_inMemoryUserInfoStorage.GetUserInfo(userId));
    }

    [HttpPost]
    public IActionResult ChangeUserInfo(UserInfo userInfo)
    {
        if (!ModelState.IsValid)
        {
            return View(userInfo);
        }

        _inMemoryUserInfoStorage.ChangeUserInfo(userInfo);
        return RedirectToAction("UserDetails", new { userId = userInfo.UserId });
    }


    public IActionResult ChangeUserRole(Guid userId)
    {
        return View(_inMemoryAccountStorage.GetAccountById(userId));
    }
    
    [HttpPost]
    public IActionResult ChangeUserRole(Account account)
    {
        if (!ModelState.IsValid)
        {
            return View(account);
        }

        _inMemoryAccountStorage.ChangeRole(account);
        return RedirectToAction("UserDetails", new { userId = account.UserId });
    }
    
    
    public IActionResult DeleteUser(Guid userId)
    {
        _inMemoryAccountStorage.Delete(userId);
        return RedirectToAction("Users");
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

    public IActionResult Products()
    {
        return View(_inMemoryProductStorage.GetAll());
    }

    public IActionResult AddNewProduct()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _inMemoryProductStorage.Add(product);
        return RedirectToAction("Products");
    }

    public IActionResult EditProduct(Guid productId)
    {
        return View(_inMemoryProductStorage.GetProduct(productId));
    }

    [HttpPost]
    public IActionResult EditProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _inMemoryProductStorage.Edit(product);
        return RedirectToAction("Products");
    }

    public IActionResult DeleteProduct(Guid productId)
    {
        _inMemoryProductStorage.Delete(productId);
        return RedirectToAction("Products");
    }
}