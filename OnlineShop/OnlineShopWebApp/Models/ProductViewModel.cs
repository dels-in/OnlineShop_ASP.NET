using OnlineShop.ReviewApi.Models;

namespace OnlineShopWebApp.Models;

public class ProductViewModel
{
    public int Id { get; set;}
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }
    public string? Source { get; set; }
    public int MetacriticScore { get; set; }
    public string Genre { get; set; }
    public IFormFile? UploadedFile { get; set; }
    public List<ReviewViewModel> Reviews { get; set; }
}