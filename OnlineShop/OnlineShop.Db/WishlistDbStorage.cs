using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class WishlistDbStorage : IStorage<Wishlist, Product>
{
    private readonly DatabaseContext _dbContext;

    public WishlistDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(Product product, string userId)
    {
        var wishlist = GetByUserId(userId);
        if (wishlist == null)
        {
            _dbContext.Wishlists.Add(new Wishlist
            {
                UserId = userId,
                Products = new() { product }
            });
        }
        else
        {
            var wishlistItem = wishlist.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (wishlistItem == null)
            {
                wishlist.Products.Add(product);
            }
        }

        _dbContext.SaveChanges();
    }

    public void Delete(Product product, string userId)
    {
        var wishlist = GetByUserId(userId);
        if (wishlist != null)
        {
            var wishlistItem = wishlist.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (wishlistItem != null)
            {
                _dbContext.Wishlists.FirstOrDefault(ci => ci == wishlist).Products.Remove(wishlistItem);
            }
        }

        _dbContext.SaveChanges();
    }

    public Wishlist GetByUserId(string userId)
    {
        return _dbContext.Wishlists
            .Include(x => x.Products)
            .FirstOrDefault(c => c.UserId == userId);
    }

    public List<Wishlist> GetAll()
    {
        return _dbContext.Wishlists.ToList();
        ;
    }

    public void Reduce(Product product, string userId)
    {
        throw new NotImplementedException();
    }

    public void AddToList(Product checkout,  List<CartItem> cartItems, string userId)
    {
        throw new NotImplementedException();
    }

    public void Clear(Wishlist parameter)
    {
        throw new NotImplementedException();
    }

    public void Edit(Guid id, OrderStatus status)
    {
        throw new NotImplementedException();
    }
}