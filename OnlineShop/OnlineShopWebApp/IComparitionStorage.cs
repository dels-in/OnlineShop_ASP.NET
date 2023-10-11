using WebApplication1.Models;

namespace WebApplication1.Controllers;

public interface IComparitionStorage
{
    void AddToComparition(Product product, string userId);
    void Delete(Product product, string userId);
    Comparition GetByUserId(string userId);
}