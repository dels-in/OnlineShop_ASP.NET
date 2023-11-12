using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryWishlistStorage : IStorage<Wishlist, ProductViewModel>
{
    private List<Wishlist> _wishlist = new();

    public void AddToList(ProductViewModel productViewModel, string userId)
    {
        var wishlist = GetByUserId(userId);
        if (wishlist == null)
        {
            _wishlist.Add(new Wishlist
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Products = new() { productViewModel }
            });
        }
        else
        {
            var wishlistItem = wishlist.Products.FirstOrDefault(ci => ci.Id == productViewModel.Id);
            if (wishlistItem == null)
            {
                wishlist.Products.Add(productViewModel);
            }
        }
    }

    public void AddToList(ProductViewModel checkout, Cart cart, string userId)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProductViewModel productViewModel, string userId)
    {
        var wishlist = GetByUserId(userId);
        if (wishlist != null)
        {
            var wishlistItem = wishlist.Products.FirstOrDefault(ci => ci.Id == productViewModel.Id);
            if (wishlistItem != null)
            {
                _wishlist.FirstOrDefault(ci => ci == wishlist).Products.Remove(wishlistItem);
            }
        }
    }

    public void Reduce(ProductViewModel productViewModel, string userId)
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