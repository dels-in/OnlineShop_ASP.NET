namespace WebApplication1.Models;

public class ProductViewModel
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int MetacriticScore { get; set; }
    public string Genre { get; set; }
}