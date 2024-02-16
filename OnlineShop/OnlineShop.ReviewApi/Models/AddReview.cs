namespace OnlineShop.ReviewApi;

public class AddReview
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; }
    public int Grade { get; set; }
}