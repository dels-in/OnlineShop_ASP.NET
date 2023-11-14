using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Storages;

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
        return View("Account", _inMemoryAccountStorage.GetAccountById(userId));
    }
}