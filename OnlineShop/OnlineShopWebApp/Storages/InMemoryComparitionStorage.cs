using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryComparitionStorage : IStorage<Comparition, Product>
{
    private List<Comparition> _comparitionList = new();

    public void AddToList(Product product, string userId)
    {
        var comparition = GetByUserId(userId);
        if (comparition == null)
        {
            _comparitionList.Add(new Comparition
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Products = new() { product }
            });
        }
        else
        {
            var comparitionItem = comparition.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (comparitionItem == null)
            {
                comparition.Products.Add(product);
            }
        }
    }

    public void Delete(Product product, string userId)
    {
        var comparition = GetByUserId(userId);
        if (comparition != null)
        {
            var comparitionItem = comparition.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (comparitionItem != null)
            {
                _comparitionList.FirstOrDefault(ci => ci == comparition).Products.Remove(comparitionItem);
            }
        }
    }

    public void Reduce(Product product, string userId)
    {
        throw new NotImplementedException();
    }

    public Comparition GetByUserId(string userId)
    {
        return _comparitionList.FirstOrDefault(c => c.UserId == userId);
    }

    public List<Comparition> GetAll()
    {
        return _comparitionList;
    }

    public void Clear(Comparition parameter)
    {
        throw new NotImplementedException();
    }

    public void Edit(Guid id, string status)
    {
        throw new NotImplementedException();
    }
}