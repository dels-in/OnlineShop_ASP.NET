using System.Text.Json;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryAccountStorage : IStorage<Account, Account>
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<Account> _accounts;

    public InMemoryAccountStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _accounts = _inMemoryFileStorage.Load<Account>("Accounts.json");
    }

    public void AddToList(Account account, string userId)
    {
        _accounts.Add(account);
        _inMemoryFileStorage.Save(_accounts, "Accounts.json");
    }

    public void Delete(Account account, string userId)
    {
        throw new NotImplementedException();
    }

    public void Reduce(Account account, string userId)
    {
        throw new NotImplementedException();
    }

    public Account GetByUserId(string userId)
    {
        return _accounts.FirstOrDefault(x => x.UserId == userId);
    }
}