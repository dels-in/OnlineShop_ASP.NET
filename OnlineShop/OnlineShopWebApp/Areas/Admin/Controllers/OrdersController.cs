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
    private readonly IStorage<Order, UserInfo> _checkoutDbStorage;

    public OrdersController(IStorage<Order, UserInfo> checkoutDbStorage)
    {
        _checkoutDbStorage = checkoutDbStorage;
    }

    public IActionResult Index()
    {
        return View(Mapping<OrderViewModel, Order>.ToViewModelList(_checkoutDbStorage.GetAll()));
    }

    public IActionResult Edit(Guid orderId)
    {
        var order = Mapping<OrderViewModel, Order>.ToViewModelList(_checkoutDbStorage.GetAll()).FirstOrDefault(o => o.OrderId == orderId);
        return View(order);
    }

    [HttpPost]
    public IActionResult Edit(Guid orderId, OrderStatusViewModel orderStatusViewModel)
    {
        _checkoutDbStorage.Edit(orderId, Mapping<OrderStatus, OrderStatusViewModel>.ToViewModel(orderStatusViewModel));
        return RedirectToAction("Index");
    }
}