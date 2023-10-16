using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Account
{
    public string UserId { get; set; }
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Passwords do not match!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    public bool IsChecked { get; set; }
}