namespace WebApplication1.Models;

public static class CartStorage
{
    private static List<Cart> _carts = new();

    public static void AddToCart(Product product, string userId)
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

    public static void Delete(Product product, string userId)
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

    public static void Reduce(Product product, string userId)
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

    public static List<CartItem> GetAll(string userId)
    {
        var cart = GetByUserId(userId);
        return cart?.CartItems ?? new();
    }

    public static Cart GetByUserId(string userId)
    {
        return _carts.FirstOrDefault(c => c.UserId == userId);
    }
}