using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models;

public class User : IdentityUser
{
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
    
    public string? Picture { get; set; }
    
    public string RoleName { get; set; }
}