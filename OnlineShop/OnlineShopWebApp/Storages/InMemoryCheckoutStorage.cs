using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryCheckoutStorage : IStorage<Order, Checkout>
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<Order> _orders;

    public InMemoryCheckoutStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _orders = _inMemoryFileStorage.Load<Order>("Orders.json");
    }

    public void AddToList(Checkout checkout, string userId)
    {
        _orders.Add(new Order
        {
            OrderId = Guid.NewGuid(),
            UserId = userId,
            DateTime = DateTime.Now,
            OrderStatus = OrderStatus.Created,
            Checkout = checkout
        });
        _inMemoryFileStorage.Save(_orders, "Orders.json");
    }

    public List<Order> GetAll()
    {
        return _orders;
    }

    public void Edit(Guid orderId, OrderStatus orderStatus)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.OrderStatus = orderStatus;
        }
    }

    public void Clear(Order parameter)
    {
        throw new NotImplementedException();
    }

    public void Delete(Checkout checkout, string userId)
    {
        throw new NotImplementedException();
    }

    public void Reduce(Checkout checkout, string userId)
    {
        throw new NotImplementedException();
    }

    public Order GetByUserId(string userId)
    {
        throw new NotImplementedException();
    }
}