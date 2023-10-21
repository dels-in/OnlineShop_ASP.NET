using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Account
{
    public string UserId { get; set; }

    [Required(ErrorMessage = "Email does not appear to be")]
    [EmailAddress(ErrorMessage = "Email does not appear to be")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password does not appear to be")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Password does not appear to be")]
    [Compare("Password", ErrorMessage = "Passwords do not match!")]
    public string ConfirmPassword { get; set; }
    public bool IsChecked { get; set; }
}