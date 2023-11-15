using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class CheckoutDbStorage : IStorage<Order, UserInfo>
{
    private readonly DatabaseContext _dbContext;

    public CheckoutDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(UserInfo userInfo, Cart cart, string userId)
    {
        _dbContext.Orders.Add(new Order
        {
            UserId = userId,
            DateTime = DateTime.Now,
            OrderStatus = OrderStatus.Created,
            UserInfo = userInfo,
            Cart = cart
        });

        _dbContext.SaveChanges();
    }

    public List<Order> GetAll()
    {
        return _dbContext.Orders.ToList();
    }

    public void Edit(Guid orderId, OrderStatus orderStatus)
    {
        var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.OrderStatus = orderStatus;
        }

        _dbContext.SaveChanges();
    }

    public void Clear(Order parameter)
    {
        throw new NotImplementedException();
    }

    public void AddToList(UserInfo product, string userId)
    {
        throw new NotImplementedException();
    }

    public void Delete(UserInfo product, string userId)
    {
        throw new NotImplementedException();
    }

    public void Reduce(UserInfo product, string userId)
    {
        throw new NotImplementedException();
    }

    public Order GetByUserId(string userId)
    {
        throw new NotImplementedException();
    }
}