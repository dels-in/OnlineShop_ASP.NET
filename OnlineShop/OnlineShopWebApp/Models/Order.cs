namespace WebApplication1.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public string UserId { get; set; }
    
    public DateTime DateTime { get; set; }

    public string OrderStatus { get; set; }
    public Checkout Checkout { get; set; }
}