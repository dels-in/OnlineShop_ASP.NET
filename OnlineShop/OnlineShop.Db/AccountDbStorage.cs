using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class AccountDbStorage : IAccountStorage
{
    private readonly DatabaseContext _dbContext;

    public AccountDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(Account account)
    {
        _dbContext.Accounts.Add(account);
        _dbContext.SaveChanges();
    }

    public Account GetAccount(string email)
    {
        return _dbContext.Accounts.FirstOrDefault(account => account.Email == email);
    }

    public List<Account> GetAll()
    {
        return _dbContext.Accounts.ToList();
    }

    public Account GetAccountById(Guid userId)
    {
        return _dbContext.Accounts.FirstOrDefault(account => account.Id == userId);
    }

    public void ChangePassword(Account account)
    {
        var accountToChange = GetAccountById(account.Id);
        if (accountToChange == null) return;

        accountToChange.Password = account.Password;
        accountToChange.ConfirmPassword = account.ConfirmPassword;

        _dbContext.SaveChanges();
    }

    public void ChangeRole(Account account)
    {
        var accountToChange = GetAccountById(account.Id);
        if (accountToChange == null) return;
        accountToChange.RoleName = account.RoleName;

        _dbContext.SaveChanges();
    }

    public void Delete(Guid userId)
    {
        _dbContext.Accounts.Remove(GetAccountById(userId));
        _dbContext.SaveChanges();
    }
}