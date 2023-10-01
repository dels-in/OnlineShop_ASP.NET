namespace WebApplication1.Models;

public static class CartStorage
{
    private static List<Product> _cart = new();

    public static void AddToCart(int id)
    {
        _cart.Add(ProductStorage.GetProduct(id));
    }

    public static void Delete(int id)
    {
        if (_cart.Exists(p => p.Id == id))
            _cart.RemoveAll(p => p.Id == id);
    }

    public static void Reduce(int id)
    {
        if (_cart.Exists(p => p.Id == id))
            _cart.Remove(_cart.FirstOrDefault(p => p.Id == id));
    }

    public static List<Product> GetAll()
    {
        return _cart;
    }


    // public static string GetCartId()
    // {
    //    
    // }
}