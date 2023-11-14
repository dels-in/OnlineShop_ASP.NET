namespace OnlineShop.Db.Models;

public class Product
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int MetacriticScore { get; set; }
    public string Genre { get; set; }
    
    public List<CartItem> CartItems { get; set; } = new();
    
    public List<Comparison> Comparisons { get; set; } = new();
    
    public List<Library> Libraries { get; set; } = new();
    
    public List<Wishlist> Wishlists { get; set; } = new();
}