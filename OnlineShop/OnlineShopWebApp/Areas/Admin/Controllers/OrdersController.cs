using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Storages;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class OrdersController : Controller
{
    private readonly IStorage<OrderViewModel, UserInfoViewModel> _checkoutDbStorage;

    public OrdersController(IStorage<OrderViewModel, UserInfoViewModel> checkoutDbStorage)
    {
        _checkoutDbStorage = checkoutDbStorage;
    }

    public IActionResult Index()
    {
        return View(_checkoutDbStorage.GetAll());
    }

    public IActionResult Edit(Guid orderId)
    {
        var order = _checkoutDbStorage.GetAll().FirstOrDefault(o => o.OrderId == orderId);
        return View(order);
    }

    [HttpPost]
    public IActionResult Edit(Guid orderId, OrderStatusViewModel orderStatusViewModel)
    {
        _checkoutDbStorage.Edit(orderId, Mapping<OrderStatus, OrderStatusViewModel>.ToViewModel(orderStatusViewModel));
        return RedirectToAction("Index");
    }
}