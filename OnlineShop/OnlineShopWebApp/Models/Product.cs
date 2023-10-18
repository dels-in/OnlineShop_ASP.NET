namespace WebApplication1.Models;

public class Product
{
    private static int _counter;
    public int Id { get; }
    public string? Name { get; set; }
    public decimal? Cost { get; set; }
    public string? Description { get; set; }
    public string? Source { get; set; }
    public int? MetacriticScore { get; set; }
    public string? Genre { get; set; }
    
    public Product(string? name, decimal cost, string? description, string? source, int metacriticScore, string? genre)
    {
        _counter++;
        Id = _counter;
        Name = name;
        Cost = cost;
        Description = description;
        Source = source;
        MetacriticScore = metacriticScore;
        Genre = genre;

    }

    public override string ToString()
    {
        return $"ID: {Id}\nName: {Name}\nCost: {Cost:C}\nDescription: {Description}";
    }
}