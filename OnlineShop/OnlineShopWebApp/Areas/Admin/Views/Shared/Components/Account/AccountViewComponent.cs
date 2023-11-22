using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Account;

public class AccountViewComponent : ViewComponent
{
    private readonly IAccountStorage _accountDbStorage;

    public AccountViewComponent(IAccountStorage accountDbStorage)
    {
        _accountDbStorage = accountDbStorage;
    }

    public IViewComponentResult Invoke(Guid userId)
    {
        return View("Account", Mapping<AccountViewModel, OnlineShop.Db.Models.Account>.ToViewModel(_accountDbStorage.GetAccountById(userId)));
    }
}