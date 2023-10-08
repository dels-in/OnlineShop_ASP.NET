using WebApplication1.Models;

namespace WebApplication1;

public interface IFileStorage
{
    void SaveProducts(List<Product> products);
}