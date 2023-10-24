using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryCartsStorage : IStorage<Cart, Product>
{
    private List<Cart> _carts = new();

    public void AddToList(Product product, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart == null)
        {
            _carts.Add(new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CartItems = new List<CartItem>
                {
                    new()
                    {
                        Product = product,
                        Quantity = 1
                    }
                }
            });
        }
        else
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem == null)
            {
                cart.CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }
        }
    }

    public void Delete(Product product, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem != null)
            {
                _carts.FirstOrDefault(ci => ci == cart).CartItems.Remove(cartItem);
            }
        }
    }

    public void Reduce(Product product, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (cartItem != null)
            {
                _carts.FirstOrDefault(ci => ci == cart).CartItems.FirstOrDefault(ci => ci == cartItem).Quantity--;
                if (cartItem.Quantity < 1)
                    _carts.FirstOrDefault(ci => ci == cart).CartItems.Remove(cartItem);
            }
        }
    }

    public Cart GetByUserId(string userId)
    {
        return _carts.FirstOrDefault(c => c.UserId == userId);
    }

    public List<Cart> GetAll()
    {
        return _carts;
    }

    public void Clear(Cart cart)
    {
        _carts.Remove(cart);
    }

    public void Edit(Guid id, string status)
    {
        throw new NotImplementedException();
    }
}