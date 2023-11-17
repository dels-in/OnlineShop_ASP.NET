using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class CheckoutDbStorage : IStorage<Order, UserInfo>
{
    private readonly DatabaseContext _dbContext;

    public CheckoutDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(UserInfo userInfo, List<CartItem> cartItems, string userId)
    {
        var newOrder = new Order
        {
            UserId = userId,
            DateTime = DateTime.Now,
            OrderStatus = OrderStatus.Created,
            UserInfo = userInfo,
            CartItems = cartItems
        };
        _dbContext.Orders.Add(newOrder);

        _dbContext.SaveChanges();
    }

    public List<Order> GetAll()
    {
        var orders = _dbContext.Orders
            .Include(x => x.UserInfo)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .ToList();
        return orders;
    }

    public void Edit(Guid orderId, OrderStatus orderStatus)
    {
        var order = _dbContext.Orders
            .Include(x => x.UserInfo)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefault(o => o.Id == orderId);
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