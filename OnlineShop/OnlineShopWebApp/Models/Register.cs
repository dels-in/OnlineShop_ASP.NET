using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Register : IAccount
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Email does not appear to be")]
    [EmailAddress(ErrorMessage = "Email does not appear to be")]
    public string Email { get; set; }
    
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password does not fit")]
    [Required(ErrorMessage = "Password does not appear to be")]
    public string Password { get; set; }
    
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password does not fit")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
    public bool IsChecked { get; set; }

    public bool IsLogin { get; set; } = false;
}