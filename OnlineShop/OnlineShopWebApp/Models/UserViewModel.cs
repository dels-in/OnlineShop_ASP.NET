using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models;

public class UserViewModel : IdentityUser
{
    [Required(ErrorMessage = "Email does not appear to be")]
    [EmailAddress(ErrorMessage = "Email does not appear to be")]
    public string Email { get; set; }

    [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password does not fit")]
    [Required(ErrorMessage = "Password does not appear to be")]
    public string Password { get; set; }

    [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password does not fit")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
    public string? Picture { get; set; }
    
    public List<Role>? Roles { get; set; }
    
    public bool IsChecked { get; set; }
    
    public string? ReturnUrl { get; set; }
}