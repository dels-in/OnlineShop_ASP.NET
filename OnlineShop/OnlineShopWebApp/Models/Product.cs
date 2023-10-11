namespace WebApplication1.Models;

public class Product
{
    private static int _counter;
    public int Id { get; }
    public string Name { get; }
    public decimal Cost { get; }
    public string Description { get; }
    public string Source { get; }
    public int MetacriticScore { get; }
    public string Genre { get; }


    public Product(string name, decimal cost, string description, string source, int metacriticScore, string genre)
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