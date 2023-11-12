namespace WebApplication1.Models;

public class Comparison
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<ProductViewModel> Products { get; set; }
}