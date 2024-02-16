namespace OnlineShopWebApp.Models;

public class ReviewViewModel
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public int UserId { get; set; }
    
    public string? Text { get; set; }
    
    public int Grade { get; set; }
    
    public DateTime CreateDate { get; set; }
}