using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public interface IStorage<T, K>
{
    void AddToList(K product, string userId);
    void AddToList(K checkout, List<CartItem> cartItems, string userId);
    void Delete(K product, string userId);
    void Reduce(K product, string userId);
    T GetByUserId(string userId);
    List<T> GetAll();
    void Clear(T parameter);
    void Edit(Guid id, OrderStatus status);
}