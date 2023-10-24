using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryAccountStorage : IAccountStorage
{
    private readonly IFileStorage _inMemoryFileStorage;
    private readonly List<IAccount> _accounts;

    public InMemoryAccountStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _accounts = _inMemoryFileStorage.Load<IAccount>("Accounts.json");
    }

    public void AddToList(IAccount parameter)
    {
        _accounts.Add(parameter);
        _inMemoryFileStorage.Save(_accounts, "Accounts.json");
    }
}