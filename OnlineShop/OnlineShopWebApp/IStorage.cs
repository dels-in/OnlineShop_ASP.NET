using WebApplication1.Models;

namespace WebApplication1.Controllers;

public interface IStorage<T>
{
    void AddToList(Product product, string userId);
    void Delete(Product product, string userId);
    void Reduce(Product product, string userId);
    T GetByUserId(string userId);
}