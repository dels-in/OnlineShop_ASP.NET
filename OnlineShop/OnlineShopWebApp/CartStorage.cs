namespace WebApplication1.Models;

public static class CartStorage
{
    private static List<Cart> _carts = new();

    public static void AddToCart(int id, string userId)
    {
        var cart = GetByUserId(userId);
        if (cart == null)
        {
            _carts.Add(new Cart()
            {
                UserId = userId,
                Products = new List<Product>() { ProductStorage.GetProduct(id) }
            });
        }
        else
        {
            _carts.Find(c => c.UserId == userId).Products.Add(ProductStorage.GetProduct(id));
        }
    }

    public static void Delete(int id, string userId)
    {
        if (_carts.Exists(c => c.Products.Exists(p => p.Id == id) & c.UserId == userId))
            _carts.Find(c => c.UserId == userId).Products.RemoveAll(p => p.Id == id);
    }

    public static void Reduce(int id, string userId)
    {
        if (_carts.Exists(c => c.Products.Exists(p => p.Id == id) & c.UserId == userId))
            _carts.Find(c => c.UserId == userId).Products
                .Remove(_carts.Find(c => c.UserId == userId).Products.FirstOrDefault(p => p.Id == id));
    }

    public static List<Product> GetAll(string userId)
    {
        var cart = _carts.FirstOrDefault(c => c.UserId == userId);
        return cart == null ? new List<Product>() : cart.Products;
    }

    private static Cart GetByUserId(string userId)
    {
        return _carts.FirstOrDefault(c => c.UserId == userId);
    }
}