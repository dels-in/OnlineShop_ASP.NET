using static System.IO.Path;

namespace WebApplication1.Models;

public static class ProductStorage
{
    private static List<Product> _products = new();

    public static List<Product> GetAll()
    {
        GetOrAdd();
        return _products;
    }

    public static Product GetProduct(string name)
    {
        GetOrAdd();
        return _products.FirstOrDefault(p => p.Name == name);
    }

    private static void GetOrAdd()
    {
        if (FileStorage.Exists("Products.txt"))
        {
            var text = FileStorage.GetResults("Products.txt");
            var paragraphs = text.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (var paragraph in paragraphs)
            {
                var lines = paragraph.Split("\n");
                var name = lines[1];
                decimal.TryParse(lines[2], out var cost);
                var description = lines[3];
                var source = lines[4];
                if (_products.Count > 0)
                {
                    var product = _products.FirstOrDefault(
                        p => p.Name == name & p.Cost == cost & p.Description == description & p.Source == source);
                    if (_products.Contains(product))
                        break;
                }

                _products.Add(new Product(name, cost, description, source));
            }
        }
        else
        {
            _products.Add(new("Katana Zero", 999,
                "Katana ZERO is a stylish " +
                "neo-noir, action-platformer featuring breakneck " +
                "action and instant-death combat. Slash, dash, " +
                "and manipulate time to unravel your past in " +
                "a beautifully brutal acrobatic display.",
                "/images/katana_zero.png"));
            _products.Add(new("Sekiro", 2999,
                "Carve your own clever path to vengeance " +
                "in the award winning adventure from developer FromSoftware, " +
                "creators of Bloodborne and the Dark Souls series. " +
                "Take Revenge. Restore Your Honor. Kill Ingeniously.",
                "/images/sekiro.jpg"));
            _products.Add(new("Starfield", 5999,
                "In this new generation role-playing " +
                "game, which takes place in space, you can create any " +
                "character and explore the universe the way you want. " +
                "Embark on a journey and uncover the greatest mystery " +
                "of humanity.",
                "/images/starfield.jpeg"));
            _products.Add(new("Tetris", 499,
                "Connected adds an all-new robust " +
                "multiplayer expansion to the huge variety of addictive and " +
                "innovative single-player modes that Tetris Effect is known " +
                "for, with all-new co-op and competitive online and local " +
                "multiplayer modes!",
                "/images/tetris.jpg"));
        }

        SaveProducts(_products);
    }

    private static void SaveProducts(List<Product> products)
    {
        FileStorage.Clear("Products.txt");
        foreach (var product in products)
        {
            Add(product);
        }
    }

    private static void Add(Product newProduct)
    {
        var textFile = Combine(Environment.CurrentDirectory, "Products.txt");
        File.AppendAllText(textFile,
            $"{newProduct.Id}\n{newProduct.Name}\n{newProduct.Cost}\n{newProduct.Description}\n{newProduct.Source}\n\n");
    }
}