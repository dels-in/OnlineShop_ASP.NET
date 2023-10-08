using WebApplication1.Models;

namespace WebApplication1;

public interface IProductStorage
{
    List<Product> GetAll();
    Product GetProduct(int productId);
    Product GetProduct(string productName);
}