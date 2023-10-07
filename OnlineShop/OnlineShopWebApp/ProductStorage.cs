using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1;

public static class ProductStorage
{
    private static readonly List<Product> _products =
        new(JsonSerializer.Deserialize<List<Product>>(
            new FileStream("Products.json", FileMode.OpenOrCreate)) ?? new ());

    public static List<Product> GetAll()
    {
        if (_products.Count == 0)
            AddToList();
        return _products;
    }

    public static Product GetProduct(int productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId);
    }

    public static Product GetProduct(string productName)
    {
        return _products.FirstOrDefault(p => p.Name == productName.ToUpper());
    }

    private static void AddToList()
    {
        _products.Add(new("Katana Zero".ToUpper(), 999,
            "Katana ZERO is a stylish " +
            "neo-noir, action-platformer featuring breakneck " +
            "action and instant-death combat. Slash, dash, " +
            "and manipulate time to unravel your past in " +
            "a beautifully brutal acrobatic display.",
            "/images/katana_zero.png"));
        _products.Add(new("Sekiro".ToUpper(), 2999,
            "Carve your own clever path to vengeance " +
            "in the award winning adventure from developer FromSoftware, " +
            "creators of Bloodborne and the Dark Souls series. " +
            "Take Revenge. Restore Your Honor. Kill Ingeniously.",
            "/images/sekiro.jpg"));
        _products.Add(new("Starfield".ToUpper(), 5999,
            "In this new generation role-playing " +
            "game, which takes place in space, you can create any " +
            "character and explore the universe the way you want. " +
            "Embark on a journey and uncover the greatest mystery " +
            "of humanity.",
            "/images/starfield.jpeg"));
        _products.Add(new("Tetris".ToUpper(), 499,
            "Connected adds an all-new robust " +
            "multiplayer expansion to the huge variety of addictive and " +
            "innovative single-player modes that Tetris Effect is known " +
            "for, with all-new co-op and competitive online and local " +
            "multiplayer modes!",
            "/images/tetris.jpg"));
        FileStorage.SaveProducts(_products);
    }
}