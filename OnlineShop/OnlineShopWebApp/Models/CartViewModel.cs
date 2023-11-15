namespace OnlineShopWebApp.Models;

public class CartViewModel
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<CartItemViewModel> CartItems { get; set; }
    public decimal SubTotal => CartItems?.Sum(i => i.Cost) ?? 0;
    public int Quantity => CartItems?.Sum(i => i.Quantity)?? 0;
}