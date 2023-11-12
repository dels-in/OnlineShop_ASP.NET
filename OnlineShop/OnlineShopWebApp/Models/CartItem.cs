namespace WebApplication1.Models;

public class CartItem
{
    public ProductViewModel ProductViewModel { get; set; }
    public int Quantity { get; set; }

    public decimal? Cost => ProductViewModel.Cost * Quantity;
}