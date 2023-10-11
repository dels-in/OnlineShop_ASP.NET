namespace WebApplication1.Models;

public class Comparition
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<Product> Products { get; set; }
}