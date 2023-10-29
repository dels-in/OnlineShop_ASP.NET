namespace WebApplication1.Models;

public class Product
{
    public Guid Id { get; set;}
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int MetacriticScore { get; set; }
    public string Genre { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}\nName: {Name}\nCost: {Cost:C}\nDescription: {Description}";
    }
}