namespace WebApplication1.Models;

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public decimal? Cost => Product.Cost * Quantity;
}