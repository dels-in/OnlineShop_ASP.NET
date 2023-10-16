namespace WebApplication1.Models;

public class Validation
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<Checkout> Checkouts { get; set;}
}