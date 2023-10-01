namespace WebApplication1.Models;

public static class CartStorage
{
    private static List<Product> _cart = new();

    public static void AddToCart(int id)
    {
        _cart.Add(ProductStorage.GetProduct(id));
    }

    public static List<Product> GetAll()
    {
        return _cart;
    }
    
}