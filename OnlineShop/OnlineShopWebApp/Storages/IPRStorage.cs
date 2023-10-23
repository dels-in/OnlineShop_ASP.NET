using WebApplication1.Models;

namespace WebApplication1.Storages;

public interface IPRStorage<T>
{
    List<T> GetAll();
    T Get(int id);
    void Add(T parameter);
    void Delete(int id);
    void Edit(int roleId, string roleName);

    void Edit(int productId, string productName, decimal productCost, string productDescription, string productSource,
        int productMetacriticScore, string productGenre);
}