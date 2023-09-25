namespace WebApplication1.Models;

public class Product
{
    private static int _counter;
    public int Id { get; }
    public string Name { get; }
    public double Cost { get; }
    public string Description { get; }

    public Product(string name, double cost, string description)
    {
        _counter++;
        Id = _counter;
        Name = name;
        Cost = cost;
        Description = description;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nName: {Name}\nCost: {Cost:C}\nDescription: {Description}";
    }
}