namespace WebApplication1.Models;

public static class ProductStorage
{
    private static List<Product> _products = new()
    {
        new("Katana Zero", 999, "Katana ZERO is a stylish " +
                                "neo-noir, action-platformer featuring breakneck " +
                                "action and instant-death combat. Slash, dash, " +
                                "and manipulate time to unravel your past in " +
                                "a beautifully brutal acrobatic display."),
        new("Sekiro", 2999, "Carve your own clever path to vengeance " +
                            "in the award winning adventure from developer FromSoftware, " +
                            "creators of Bloodborne and the Dark Souls series. " +
                            "Take Revenge. Restore Your Honor. Kill Ingeniously."),
        new("Starfield", 5999, "In this new generation role-playing " +
                               "game, which takes place in space, you can create any " +
                               "character and explore the universe the way you want. " +
                               "Embark on a journey and uncover the greatest mystery " +
                               "of humanity."),
        new("Tetris", 499, "Connected adds an all-new robust " +
                           "multiplayer expansion to the huge variety of addictive and " +
                           "innovative single-player modes that Tetris Effect is known " +
                           "for, with all-new co-op and competitive online and local " +
                           "multiplayer modes!")
    };

    public static List<Product> GetAllProducts() => _products;

    public static Product GetOneProduct(int id) => _products.FirstOrDefault(p=> p.Id == id);
}