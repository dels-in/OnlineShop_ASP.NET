using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IProductStorage
{
    List<Product> GetAll();
    Product GetProduct(Guid productId);
    void Add(Product product);
    void Delete(Guid productId);
    void Edit(Product product);
}