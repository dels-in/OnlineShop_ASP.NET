using WebApplication1.Models;

namespace WebApplication1.Controllers;

public interface IWishlistStorage
{
    void AddToWishlist(Product product, string userId);
    void Delete(Product product, string userId);
    Wishlist GetByUserId(string userId);
}