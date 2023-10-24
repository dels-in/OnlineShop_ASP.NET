namespace WebApplication1.Models;

public class Library
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<Product> Products { get; set; }
}