using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class UserInfo
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Your first name does not fit")]
    [Required(ErrorMessage = "First name does not appear to be")]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 2, ErrorMessage = "Your last name does not fit")]
    [Required(ErrorMessage = "Last name does not appear to be")]
    public string LastName { get; set; }

    [StringLength(40, MinimumLength = 4, ErrorMessage = "Your email does not fit")]
    [Required(ErrorMessage = "Email does not appear to be")]
    [EmailAddress(ErrorMessage = "Email does not appear to be")]
    public string Email { get; set; }

    [StringLength(30, MinimumLength = 2, ErrorMessage = "Your street does not fit")]
    [Required(ErrorMessage = "Street does not appear to be")]
    public string Address { get; set; }

    [StringLength(30, MinimumLength = 2, ErrorMessage = "Your apartment does not fit")]
    [Required(ErrorMessage = "Apartment does not appear to be")]
    public string Address2 { get; set; }

    [StringLength(30, MinimumLength = 2, ErrorMessage = "Your city does not fit")]
    [Required(ErrorMessage = "City does not appear to be")]
    public string City { get; set; }

    [Required(ErrorMessage = "Region does not appear to be")]
    public string Region { get; set; }

    [StringLength(7, MinimumLength = 7, ErrorMessage = "Your postcode does not fit")]
    public string PostCode { get; set; } = "000 000";

    [Required(ErrorMessage = "State does not appear to be")]
    public bool IsChecked { get; set; }
}