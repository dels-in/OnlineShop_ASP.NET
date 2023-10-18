using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IAccountStorage
{
    public void AddToList(Account account);
}