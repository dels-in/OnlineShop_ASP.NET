using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Account
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
    public string ConfirmPassword { get; set; }
    public bool IsChecked { get; set; }
}