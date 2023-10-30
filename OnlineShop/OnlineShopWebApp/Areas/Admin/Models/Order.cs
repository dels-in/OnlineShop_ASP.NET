namespace WebApplication1.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public string UserId { get; set; }

    public DateTime DateTime { get; set; }

    public OrderStatus OrderStatus { get; set; }
    public Checkout Checkout { get; set; }
    
    public List<CartItem> CartItems { get; set; }

    public decimal Cost => CartItems?.Sum(i => i.Cost) ?? 0;
}