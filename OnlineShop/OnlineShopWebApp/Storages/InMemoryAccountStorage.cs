using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryAccountStorage : IAccountStorage
{
    private readonly IFileStorage _inMemoryFileStorage;
    private readonly List<Account> _accounts;

    public InMemoryAccountStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _accounts = _inMemoryFileStorage.Load<Account>("Accounts.json");
    }

    public void AddToList(Account account)
    {
        _accounts.Add(account);
        _inMemoryFileStorage.Save(_accounts, "Accounts.json");
    }

    public Account GetAccount(string email)
    {
        return _accounts.FirstOrDefault(account => account.Email == email);
    }

    public List<Account> GetAll()
    {
        return _accounts;
    }

    public Account GetAccountById(Guid userId)
    {
        return _accounts.FirstOrDefault(account => account.UserId == userId);
    }

    public void ChangePassword(Account account)
    {
        var accountToChange = GetAccountById(account.UserId);
        if (accountToChange == null) return;
        accountToChange.Password = account.Password;
        accountToChange.ConfirmPassword = account.ConfirmPassword;
    }

    public void ChangeRole(Account account)
    {
        var accountToChange = GetAccountById(account.UserId);
        if (accountToChange == null) return;
        accountToChange.RoleName = account.RoleName;
    }

    public void Delete(Guid userId)
    {
        _accounts.Remove(GetAccountById(userId));
    }
}