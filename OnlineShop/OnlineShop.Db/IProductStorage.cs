using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public interface IProductStorage
{
    List<Product> GetAll();
    Product GetProduct(Guid productId);
    void Add(Product product);
    void Delete(Guid productId);
    void Edit(Product product);
}