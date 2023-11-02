using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;
using WebApplication1.Storages;

namespace WebApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class OrdersController : Controller
{
    private readonly IStorage<Order, UserInfo> _inMemoryCheckoutStorage;

    public OrdersController(IStorage<Order, UserInfo> inMemoryCheckoutStorage)
    {
        _inMemoryCheckoutStorage = inMemoryCheckoutStorage;
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
}