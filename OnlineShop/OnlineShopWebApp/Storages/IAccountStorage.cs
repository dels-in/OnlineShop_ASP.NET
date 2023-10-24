using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IAccountStorage
{
    public void AddToList(IAccount parameter);
    public IAccount GetAccount(string email);
    public bool IsAccountExists(string email);
}