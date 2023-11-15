namespace OnlineShop.Db.Models;

public class Comparison
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<Product> Products { get; set; } = new();
}