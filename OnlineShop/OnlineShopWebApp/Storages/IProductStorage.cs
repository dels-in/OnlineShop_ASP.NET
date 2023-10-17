using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IProductStorage
{
    List<Product> GetAll();
    Product GetProduct(int productId);
}