using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryComparisonStorage : IStorage<Comparison, Product>
{
    private List<Comparison> _comparisonList = new();

    public void AddToList(Product product, string userId)
    {
        var comparison = GetByUserId(userId);
        if (comparison == null)
        {
            _comparisonList.Add(new Comparison
            {
                Id = Guid.NewGuid(),
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
    }

    public void AddToList(Product checkout, Cart cart, string userId)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product product, string userId)
    {
        var comparison = GetByUserId(userId);
        if (comparison != null)
        {
            var comparisonItem = comparison.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (comparisonItem != null)
            {
                _comparisonList.FirstOrDefault(ci => ci == comparison).Products.Remove(comparisonItem);
            }
        }
    }

    public void Reduce(Product product, string userId)
    {
        throw new NotImplementedException();
    }

    public Comparison GetByUserId(string userId)
    {
        return _comparisonList.FirstOrDefault(c => c.UserId == userId);
    }

    public List<Comparison> GetAll()
    {
        return _comparisonList;
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