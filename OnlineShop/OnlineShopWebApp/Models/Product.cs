namespace WebApplication1.Models;

public class Product
{
    public int Id { get; } = 0;
    public string Name { get;}
    public double Cost { get; }
    public string Description { get; }

    public Product(string name, double cost, string description)
    {
        Id++;
        Name = name;
        Cost = cost;
        Description = description;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nName: {Name}\nCost: {Cost}\nDescription: {Description}";
    }
}