namespace OnlineShop.Db.Models;

public class Account
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }

    public bool IsChecked { get; set; }

    public string RoleName { get; set; }
    
    public string? Picture { get; set; }
}