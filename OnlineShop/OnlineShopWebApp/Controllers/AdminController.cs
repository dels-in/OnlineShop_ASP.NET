using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class AdminController : Controller
{
    private readonly IProductStorage _inMemoryProductStorage;
    private readonly IStorage<Order, Checkout> _inMemoryCheckoutStorage;
    private readonly IRoleStorage _inMemoryRoleStorage;

    public AdminController(IProductStorage inMemoryProductStorage,
        IStorage<Order, Checkout> inMemoryCheckoutStorage, IRoleStorage inMemoryRoleStorage)
    {
        _inMemoryProductStorage = inMemoryProductStorage;
        _inMemoryCheckoutStorage = inMemoryCheckoutStorage;
        _inMemoryRoleStorage = inMemoryRoleStorage;
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
        return View();
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
            RedirectToAction("AddNewProduct", product);
        }

        _inMemoryProductStorage.Add(product);
        return RedirectToAction("Products");
    }

    public IActionResult EditProduct(Guid productId)
    {
        return View(_inMemoryProductStorage.GetProduct(productId));
    }

    [HttpPost]
    public IActionResult EditProduct(Guid productId, Product product)
    {
        if (!ModelState.IsValid)
        {
            RedirectToAction("EditProduct", new { productId });
        }

        _inMemoryProductStorage.Edit(productId, product);
        return RedirectToAction("Products");
    }

    public IActionResult DeleteProduct(Guid productId)
    {
        _inMemoryProductStorage.Delete(productId);
        return RedirectToAction("Products");
    }
}