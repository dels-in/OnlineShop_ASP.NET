using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public interface IProductStorage
{
    List<Product> GetAll();
    Product GetProduct(int productId);
    void Add(Product product);
    void Delete(int productId);
    void Edit(Product product);
}