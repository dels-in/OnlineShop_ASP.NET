using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IProductStorage
{
    List<Product> GetAll();
    Product GetProduct(int productId);
    void Add(Product product);
    void Delete(int productId);
    void Edit(int productId, string productName, decimal productCost, string productDescription, string productSource, int productMetacriticScore, string productGenre);
}