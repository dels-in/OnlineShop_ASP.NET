using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryComparitionStorage : IComparitionStorage
{
    private List<Comparition> _comparitionList = new();

    public void AddToComparition(Product product, string userId)
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

    public Comparition GetByUserId(string userId)
    {
        return _comparitionList.FirstOrDefault(c => c.UserId == userId);
    }
}