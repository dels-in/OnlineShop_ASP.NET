using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class CartsDbStorage : IStorage<Cart, Product>
{
    private readonly DatabaseContext _dbContext;

    public CartsDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(Product product, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart == null)
        {
            var newCart = new Cart
            {
                UserId = userId,
            };
            newCart.CartItems = new List<CartItem>
            {
                new()
                {
                    Product = product,
                    Quantity = 1,
                    Cart = newCart,
                }
            };

            _dbContext.Carts.Add(newCart);
        }
        else
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem == null)
            {
                cart.CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1,
                    Cart = cart,
                });
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        _dbContext.SaveChanges();
    }

    public void Delete(Product product, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem != null)
            {
                _dbContext.Carts.FirstOrDefault(ci => ci == cart).CartItems.Remove(cartItem);
            }
        }

        _dbContext.SaveChanges();
    }

    public void Reduce(Product product, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem != null)
            {
                _dbContext.Carts.FirstOrDefault(ci => ci == cart).CartItems.FirstOrDefault(ci => ci == cartItem)
                    .Quantity--;
                if (cartItem.Quantity < 1)
                    _dbContext.Carts.FirstOrDefault(ci => ci == cart).CartItems.Remove(cartItem);
            }
        }

        _dbContext.SaveChanges();
    }

    public Cart GetByUserId(string userId)
    {
        return _dbContext.Carts
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefault(c => c.UserId == userId);
    }

    public List<Cart> GetAll()
    {
        return _dbContext.Carts.ToList();
    }

    public void Clear(Cart cart)
    {
        _dbContext.Carts.Remove(cart);
        _dbContext.SaveChanges();
    }

    public void AddToList(Product checkout, Cart cart, string userId)
    {
        throw new NotImplementedException();
    }

    public void Edit(Guid id, OrderStatus status)
    {
        throw new NotImplementedException();
    }
}