using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Controllers;

public class AdminController : Controller
{
    private readonly IPRStorage<Product> _inMemoryProductStorage;
    private readonly IStorage<Order, Checkout> _inMemoryCheckoutStorage;
    private readonly IPRStorage<Role> _inMemoryRoleStorage;

    public AdminController(IPRStorage<Product> inMemoryProductStorage,
        IStorage<Order, Checkout> inMemoryCheckoutStorage, IPRStorage<Role> inMemoryRoleStorage)
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
    public IActionResult EditOrder(Guid orderId, string orderStatus)
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
    public IActionResult AddNewRole(string roleName)
    {
        var role = new Role(roleName);
        _inMemoryRoleStorage.Add(role);
        return RedirectToAction("Roles");
    }

    public IActionResult EditRole(int roleId)
    {
        return View(_inMemoryRoleStorage.Get(roleId));
    }

    [HttpPost]
    public IActionResult EditRole(int roleId, string roleName)
    {
        _inMemoryRoleStorage.Edit(roleId, roleName);
        return RedirectToAction("Roles");
    }

    public IActionResult DeleteRole(int roleId)
    {
        _inMemoryRoleStorage.Delete(roleId);
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
    public IActionResult AddNewProduct(string productName, decimal productCost,
        string productDescription, string productSource,
        int productMetacriticScore, string productGenre)
    {
        var product = new Product(productName, productCost, productDescription, productSource, productMetacriticScore,
            productGenre);
        _inMemoryProductStorage.Add(product);
        return RedirectToAction("Products");
    }

    public IActionResult EditProduct(int productId)
    {
        return View(_inMemoryProductStorage.Get(productId));
    }

    [HttpPost]
    public IActionResult EditProduct(int productId, string productName, decimal productCost,
        string productDescription, string productSource,
        int productMetacriticScore, string productGenre)
    {
        _inMemoryProductStorage.Edit(productId, productName, productCost, productDescription, productSource,
            productMetacriticScore, productGenre);
        return RedirectToAction("Products");
    }

    public IActionResult DeleteProduct(int productId)
    {
        _inMemoryProductStorage.Delete(productId);
        return RedirectToAction("Products");
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}