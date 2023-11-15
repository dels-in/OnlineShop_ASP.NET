using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Storages;

public interface IAccountStorage
{
    void AddToList(Account parameter);
    Account GetAccount(string email);
    List<Account> GetAll();
    Account GetAccountById(Guid userId);
    void ChangePassword(Account account);
    void ChangeRole(Account account);
    void Delete(Guid userId);
}