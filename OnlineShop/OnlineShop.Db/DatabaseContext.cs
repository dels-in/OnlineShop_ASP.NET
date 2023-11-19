using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Comparison> Comparisons { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new List<Product>
        {
            new()
            {
                Id = 1,
                Name = "Football Manager 2024".ToUpper(), Cost = 4499,
                Description =
                    "Build your dream squad, outsmart your rivals and experience the thrill of big European nights in the UEFA Champions League. Progress never stops when you’re pursuing footballing greatness.",
                Source = "/images/fm2024.jpg", MetacriticScore = 84, Genre = "Simulation"
            },
            new()
            {
                Id = 2,
                Name = "The Legend of Zelda: Tears of The Kingdom".ToUpper(), Cost = 8999,
                Description =
                    "In this sequel to the Legend of Zelda: Breath of the Wild game, you’ll decide your own path through the sprawling landscapes of Hyrule and the islands floating in the vast skies above.",
                Source = "/images/zelda.jpg", MetacriticScore = 96, Genre = "Action"
            },
            new()
            {
                Id = 3,
                Name = "Sekiro".ToUpper(), Cost = 2999,
                Description =
                    "Carve your own clever path to vengeance in the award winning adventure from developer FromSoftware, creators of Bloodborne and the Dark Souls series. Take Revenge. Restore Your Honor. Kill Ingeniously.",
                Source = "/images/sekiro.jpg", MetacriticScore = 90, Genre = "Action"
            },
            new()
            {
                Id = 4,
                Name = "Starfield".ToUpper(), Cost = 5999,
                Description =
                    "In this new generation role-playing game, which takes place in space, you can create any character and explore the universe the way you want. Embark on a journey and uncover the greatest mystery of humanity.",
                Source = "/images/starfield.jpeg", MetacriticScore = 83, Genre = "RPG"
            },
            new()
            {
                Id = 5,
                Name = "Tetris".ToUpper(), Cost = 499,
                Description =
                    "Connected adds an all-new robust multiplayer expansion to the huge variety of addictive and innovative single-player modes that Tetris Effect is known for, with all-new co-op and online or local multiplayer modes!",
                Source = "/images/tetris.jpg", MetacriticScore = 78, Genre = "Puzzle"
            },
            new()
            {
                Id = 6,
                Name = "Baldur's Gate 3".ToUpper(), Cost = 4999,
                Description =
                    "Baldur’s Gate 3 is a story-rich, party-based RPG set in the universe of Dungeons & Dragons, where your choices shape a tale of fellowship and betrayal, survival and sacrifice, and the lure of absolute power.",
                Source = "/images/baldursgate.jpeg", MetacriticScore = 96, Genre = "RPG"
            },
            new()
            {
                Id = 7,
                Name = "Dota 2".ToUpper(), Cost = 199,
                Description =
                    "Every day, millions of players worldwide enter battle as one of over a hundred heroes. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has taken on a life of its own.",
                Source = "/images/dota2.jpg", MetacriticScore = 96, Genre = "MOBA"
            },
            new()
            {
                Id = 8,
                Name = "Red Dead Redemption 2".ToUpper(), Cost = 5999,
                Description =
                    "RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.",
                Source = "/images/rdr2.png", MetacriticScore = 97, Genre = "Action"
            },
            new()
            {
                Id = 9,
                Name = "Katana Zero".ToUpper(), Cost = 999,
                Description =
                    "Katana ZERO is a stylish neo-noir, platformer featuring breakneck action and death combat. Slash, dash, and manipulate time to unravel your past in a beautifully brutal acrobatic display.",
                Source = "/images/katana_zero.png", MetacriticScore = 83, Genre = "Platformer"
            },
            new()
            {
                Id = 10,
                Name = "Dark Souls".ToUpper(), Cost = 3499,
                Description =
                    "Then, there was fire. Re-experience the critically acclaimed, genre-defining game that started it all. Beautifully remastered, return to Lordran in stunning high-definition detail running at 60fps.",
                Source = "/images/dark_souls.jpg", MetacriticScore = 84, Genre = "Souls-like"
            },
            new()
            {
                Id = 11,
                Name = "The Last Of Us".ToUpper(), Cost = 999,
                Description =
                    "Experience the emotional storytelling and unforgettable characters in The Last of Us. The action of the games in the series takes place in a post-apocalyptic future on the territory of the former United States of America after a global epidemic caused by a dangerously mutated mushroom cordyceps.",
                Source = "/images/tlou.jpeg", MetacriticScore = 95, Genre = "Action-adventure"
            },
            new()
            {
                Id = 12,
                Name = "Disco Elysium".ToUpper(), Cost = 1999,
                Description =
                    "Disco Elysium - The Final Cut is a groundbreaking role playing game. You’re a detective with a unique skill system at your disposal and a whole city to carve your path across. Interrogate unforgettable characters, crack murders or take bribes. Become a hero or an absolute disaster of a human being.",
                Source = "/images/disco_elysium.jpeg", MetacriticScore = 91, Genre = "RPG"
            }
        });

        modelBuilder.Entity<Role>().HasData(new List<Role>
        {
            new()
            {
                Id = 1,
                RoleName = "Admin",
            },
            new()
            {
                Id = 2,
                RoleName = "User"
            }
        });
    }
}