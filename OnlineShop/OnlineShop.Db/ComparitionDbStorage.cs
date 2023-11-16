using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class ComparisonDbStorage : IStorage<Comparison, Product>
{
    private readonly DatabaseContext _dbContext;

    public ComparisonDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(Product product, string userId)
    {
        var comparison = GetByUserId(userId);
        if (comparison == null)
        {
            _dbContext.Comparisons.Add(new Comparison
            {
                UserId = userId,
                Products = new() { product }
            });
        }
        else
        {
            var comparisonItem = comparison.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (comparisonItem == null)
            {
                comparison.Products.Add(product);
            }
        }

        _dbContext.SaveChanges();
    }

    public void Delete(Product product, string userId)
    {
        var comparison = GetByUserId(userId);
        if (comparison != null)
        {
            var comparisonItem = comparison.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (comparisonItem != null)
            {
                _dbContext.Comparisons.FirstOrDefault(ci => ci == comparison).Products.Remove(comparisonItem);
            }
        }

        _dbContext.SaveChanges();
    }

    public Comparison GetByUserId(string userId)
    {
        return _dbContext.Comparisons
            .Include(x=>x.Products)
            .FirstOrDefault(c => c.UserId == userId);
    }

    public List<Comparison> GetAll()
    {
        return _dbContext.Comparisons.ToList();
    }

    public void AddToList(Product checkout, Cart cart, string userId)
    {
        throw new NotImplementedException();
    }

    public void Reduce(Product product, string userId)
    {
        throw new NotImplementedException();
    }

    public void Clear(Comparison parameter)
    {
        throw new NotImplementedException();
    }

    public void Edit(Guid id, OrderStatus status)
    {
        throw new NotImplementedException();
    }
}