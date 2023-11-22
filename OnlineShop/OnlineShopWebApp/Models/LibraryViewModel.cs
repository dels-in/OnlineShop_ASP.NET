namespace OnlineShopWebApp.Models;

public class LibraryViewModel
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; }
    public List<ProductViewModel> Products { get; set; }
}