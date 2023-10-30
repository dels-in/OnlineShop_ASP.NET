using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryWishlistStorage : IStorage<Wishlist, Product>
{
    private List<Wishlist> _wishlist = new();

    public void AddToList(Product product, string userId)
    {
        var wishlist = GetByUserId(userId);
        if (wishlist == null)
        {
            _wishlist.Add(new Wishlist
            {
                Id = Guid.NewGuid(),
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
    }

    public void AddToList(Product checkout, Cart cart, string userId)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product product, string userId)
    {
        var wishlist = GetByUserId(userId);
        if (wishlist != null)
        {
            var wishlistItem = wishlist.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (wishlistItem != null)
            {
                _wishlist.FirstOrDefault(ci => ci == wishlist).Products.Remove(wishlistItem);
            }
        }
    }

    public void Reduce(Product product, string userId)
    {
        throw new NotImplementedException();
    }

    public Wishlist GetByUserId(string userId)
    {
        return _wishlist.FirstOrDefault(c => c.UserId == userId);
    }

    public List<Wishlist> GetAll()
    {
        return _wishlist;
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