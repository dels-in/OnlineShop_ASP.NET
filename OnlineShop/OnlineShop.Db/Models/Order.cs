namespace OnlineShop.Db.Models;

public class Order
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime DateTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public UserInfo UserInfo { get; set; }
    public Cart Cart { get; set; }
}