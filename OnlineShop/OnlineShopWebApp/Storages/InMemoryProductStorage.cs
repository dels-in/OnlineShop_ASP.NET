using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryProductStorage : IProductStorage
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<Product> _products;

    public InMemoryProductStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _products = _inMemoryFileStorage.Load<Product>("Products.json");
    }

    public List<Product> GetAll()
    {
        if (_products.Count == 0)
            AddToList();
        return _products;
    }

    public Product GetProduct(int productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId);
    }

    private void AddToList()
    {
        _products.Add(new("Katana Zero".ToUpper(), 999,
            "Katana ZERO is a stylish " +
            "neo-noir, action-platformer featuring breakneck " +
            "action and instant-death combat. Slash, dash, " +
            "and manipulate time to unravel your past in " +
            "a beautifully brutal acrobatic display.",
            "/images/katana_zero.png", 83, "Platformer"));
        _products.Add(new("The Legend of Zelda: Tears of The Kingdom".ToUpper(), 8999,
            "In this sequel to the Legend of Zelda: Breath of the Wild game, you’ll decide your own path " +
            "through the sprawling landscapes of Hyrule and the islands floating in the vast skies above.",
            "/images/zelda.jpg", 96, "Action"));
        _products.Add(new("Sekiro".ToUpper(), 2999,
            "Carve your own clever path to vengeance " +
            "in the award winning adventure from developer FromSoftware, " +
            "creators of Bloodborne and the Dark Souls series. " +
            "Take Revenge. Restore Your Honor. Kill Ingeniously.",
            "/images/sekiro.jpg", 90, "Action"));
        _products.Add(new("Starfield".ToUpper(), 5999,
            "In this new generation role-playing " +
            "game, which takes place in space, you can create any " +
            "character and explore the universe the way you want. " +
            "Embark on a journey and uncover the greatest mystery " +
            "of humanity.",
            "/images/starfield.jpeg", 83,  "RPG"));
        _products.Add(new("Tetris".ToUpper(), 499,
            "Connected adds an all-new robust " +
            "multiplayer expansion to the huge variety of addictive and " +
            "innovative single-player modes that Tetris Effect is known " +
            "for, with all-new co-op and competitive online and local " +
            "multiplayer modes!",
            "/images/tetris.jpg", 61, "Puzzle"));
        _products.Add(new("Baldur's Gate 3".ToUpper(), 4999,
            "Baldur’s Gate 3 is a story-rich, party-based RPG set in the universe of Dungeons & Dragons, " +
            "where your choices shape a tale of fellowship and betrayal, survival and sacrifice, " +
            "and the lure of absolute power.",
            "/images/baldursgate.jpeg", 96, "RPG"));
        _inMemoryFileStorage.Save(_products, "Products.json");
    }
}