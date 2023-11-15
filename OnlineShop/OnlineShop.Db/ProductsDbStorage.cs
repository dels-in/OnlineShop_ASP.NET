using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class ProductsDbStorage : IProductStorage
{
    private readonly DatabaseContext _dbContext;

    public ProductsDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> GetAll()
    {
        if (_dbContext.Products.ToList().Count == 0)
            AddToList();
        return _dbContext.Products.ToList();
    }

    public Product GetProduct(Guid productId)
    {
        return _dbContext.Products.FirstOrDefault(p => p.Id == productId);
    }

    public void Add(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void Edit(Product product)
    {
        var productToChange = GetProduct(product.Id);
        if (productToChange == null) return;
        productToChange.Name = product.Name;
        productToChange.Cost = product.Cost;
        productToChange.Description = product.Description;
        productToChange.Source = product.Source;
        productToChange.MetacriticScore = product.MetacriticScore;
        productToChange.Genre = product.Genre;
        _dbContext.SaveChanges();
    }

    public void Delete(Guid productId)
    {
        _dbContext.Products.Remove(GetProduct(productId));
        _dbContext.SaveChanges();
    }

    private void AddToList()
    {
        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Katana Zero".ToUpper(), Cost = 999,
            Description = "Katana ZERO is a stylish " +
                          "neo-noir, action-platformer featuring breakneck " +
                          "action and instant-death combat. Slash, dash, " +
                          "and manipulate time to unravel your past in " +
                          "a beautifully brutal acrobatic display.",
            Source = "/images/katana_zero.png", MetacriticScore = 83, Genre = "Platformer"
        });
        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "The Legend of Zelda: Tears of The Kingdom".ToUpper(), Cost = 8999,
            Description = "In this sequel to the Legend of Zelda: Breath of the Wild game, " +
                          "you’ll decide your own path through the sprawling landscapes of Hyrule " +
                          "and the islands floating in the vast skies above.",
            Source = "/images/zelda.jpg", MetacriticScore = 96, Genre = "Action"
        });
        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Sekiro".ToUpper(), Cost = 2999,
            Description = "Carve your own clever path to vengeance " +
                          "in the award winning adventure from developer FromSoftware, " +
                          "creators of Bloodborne and the Dark Souls series. " +
                          "Take Revenge. Restore Your Honor. Kill Ingeniously.",
            Source = "/images/sekiro.jpg", MetacriticScore = 90, Genre = "Action"
        });
        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Starfield".ToUpper(), Cost = 5999,
            Description = "In this new generation role-playing " +
                          "game, which takes place in space, you can create any " +
                          "character and explore the universe the way you want. " +
                          "Embark on a journey and uncover the greatest mystery " +
                          "of humanity.",
            Source = "/images/starfield.jpeg", MetacriticScore = 83, Genre = "RPG"
        });
        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Tetris".ToUpper(), Cost = 499,
            Description = "Connected adds an all-new robust " +
                          "multiplayer expansion to the huge variety of addictive and " +
                          "innovative single-player modes that Tetris Effect is known for, " +
                          "with all-new co-op and competitive online and local multiplayer modes!",
            Source = "/images/tetris.jpg", MetacriticScore = 61, Genre = "Puzzle"
        });

        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Baldur's Gate 3".ToUpper(), Cost = 4999,
            Description =
                "Baldur’s Gate 3 is a story-rich, party-based RPG set in the universe " +
                "of Dungeons & Dragons, where your choices shape a tale of fellowship " +
                "and betrayal, survival and sacrifice, and the lure of absolute power.",
            Source = "/images/baldursgate.jpeg", MetacriticScore = 96, Genre = "RPG"
        });
    }
}