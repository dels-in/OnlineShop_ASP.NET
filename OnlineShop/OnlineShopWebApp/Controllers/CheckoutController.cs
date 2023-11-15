using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Storages;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Controllers;

public class CheckoutController : Controller
{
    private readonly IStorage<Cart, Product> _cartsDbStorage;
    private readonly IStorage<Order, UserInfo> _checkoutDbStorage;


    public CheckoutController(IStorage<Cart, Product> cartsDbStorage,
        IStorage<Order, UserInfo> checkoutDbStorage)
    {
        _cartsDbStorage = cartsDbStorage;
        _checkoutDbStorage = checkoutDbStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(UserInfoViewModel userInfoViewModel)
    {
        var userId = GetUserId();
        userInfoViewModel.UserId = Guid.Parse(userId);
        var cart = _cartsDbStorage.GetByUserId(userId);
        try
        {
            if (HasDigits(userInfoViewModel.FirstName) || HasDigits(userInfoViewModel.LastName) ||
                HasDigits(userInfoViewModel.City))
            {
                ModelState.AddModelError("", "Names or City cannot contain digits");
            }

            if (!ModelState.IsValid)
                return View(userInfoViewModel);

            _checkoutDbStorage.AddToList(Mapping<UserInfo, UserInfoViewModel>.ToViewModel(userInfoViewModel), cart,
                userId);
        }
        catch (NotImplementedException)
        {
            // ignored
        }
        
        return RedirectToAction("Checkout", new {cart.UserId});
    }

    public IActionResult Checkout(string userId)
    {
        var cart = _cartsDbStorage.GetByUserId(userId);
        var cartViewModel = Mapping<CartViewModel, Cart>.ToViewModel(cart);
        return View(cartViewModel);
    }
    
    private bool HasDigits(string str)
    {
        return str.Any(c => c >= '0' && c <= '9');
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}