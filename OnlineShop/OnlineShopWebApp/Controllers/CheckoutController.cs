using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private readonly IStorage<Cart, Product> _cartsDbStorage;
    private readonly IStorage<Order, UserInfo> _checkoutDbStorage;
    private readonly UserManager<User> _userManager;
    private readonly IUserInfoStorage _userInfoDbStorage;

    public CheckoutController(IStorage<Cart, Product> cartsDbStorage,
        IStorage<Order, UserInfo> checkoutDbStorage, UserManager<User> userManager, IUserInfoStorage userInfoDbStorage)
    {
        _cartsDbStorage = cartsDbStorage;
        _checkoutDbStorage = checkoutDbStorage;
        _userManager = userManager;
        _userInfoDbStorage = userInfoDbStorage;
    }

    public IActionResult Index()
    {
        var user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
        var userInfoViewModel = _userInfoDbStorage.GetUserInfo(Guid.Parse(user.Id));
        return View(Mapping<UserInfoViewModel, UserInfo>.ToViewModel(userInfoViewModel));
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

            _checkoutDbStorage.AddToList(Mapping<UserInfo, UserInfoViewModel>.ToViewModel(userInfoViewModel),
                cart.CartItems,
                userId);
        }
        catch (NotImplementedException)
        {
            // ignored
        }

        return RedirectToAction("Checkout", new { cart.UserId });
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