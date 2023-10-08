using WebApplication1.Models;

namespace WebApplication1;

public interface ICartsStorage
{
    void AddToCart(Product product, string userId);
    void Delete(Product product, string userId);
    void Reduce(Product product, string userId);
    Cart GetByUserId(string userId);
}