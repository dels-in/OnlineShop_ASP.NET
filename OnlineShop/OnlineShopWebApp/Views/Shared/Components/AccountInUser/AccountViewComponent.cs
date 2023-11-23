using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace OnlineShopWebApp.Views.Shared.Components.AccountInUser;

public class AccountInUserViewComponent : ViewComponent
{
    private readonly IAccountStorage _accountDbStorage;

    public AccountInUserViewComponent(IAccountStorage accountDbStorage)
    {
        _accountDbStorage = accountDbStorage;
    }

    public IViewComponentResult Invoke()
    {
        var account = Mapping<AccountViewModel, OnlineShop.Db.Models.Account>.ToViewModel(_accountDbStorage.GetAccountById(Guid.Parse(GetUserId())));
        return View("AccountInUser", account);
    }
    
    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature?.AnonymousId ?? "007";
    }
}