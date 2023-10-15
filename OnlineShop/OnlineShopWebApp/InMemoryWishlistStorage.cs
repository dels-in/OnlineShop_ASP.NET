using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryWishlistStorage : IStorage<Wishlist>
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
}