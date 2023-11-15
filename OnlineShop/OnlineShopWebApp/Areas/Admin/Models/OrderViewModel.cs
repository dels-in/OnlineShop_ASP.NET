using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Models;

public class OrderViewModel
{
    public Guid OrderId { get; set; }
    public string UserId { get; set; }

    public DateTime DateTime { get; set; }

    public OrderStatusViewModel OrderStatusViewModel { get; set; }
    public UserInfoViewModel UserInfoViewModel { get; set; }
    
    public List<CartItemViewModel> CartItems { get; set; }

    public decimal Cost => CartItems?.Sum(i => i.Cost) ?? 0;
}