using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models;

public class User : IdentityUser
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
    
    public string? Picture { get; set; }
}