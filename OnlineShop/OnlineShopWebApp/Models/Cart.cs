namespace WebApplication1.Models;

public class Cart
{
    public Guid Id { get; set; }
    public string UserId { get; set; }

    public List<CartItem> CartItems { get; set; }

    public decimal SubTotal => CartItems.Sum(i => i.Cost);
}