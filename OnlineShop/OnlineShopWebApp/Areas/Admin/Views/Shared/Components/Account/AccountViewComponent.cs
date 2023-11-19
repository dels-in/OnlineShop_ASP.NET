using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Views.Shared.Components.Account;

public class AccountViewComponent : ViewComponent
{
    private readonly IAccountStorage _inMemoryAccountStorage;

    public AccountViewComponent(IAccountStorage inMemoryAccountStorage)
    {
        _inMemoryAccountStorage = inMemoryAccountStorage;
    }

    public IViewComponentResult Invoke(Guid userId)
    {
        return View("Account", Mapping<AccountViewModel, OnlineShop.Db.Models.Account>.ToViewModel(_inMemoryAccountStorage.GetAccountById(userId)));
    }
}