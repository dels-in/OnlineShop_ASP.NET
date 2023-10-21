using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Checkout
{
    [Required(ErrorMessage = "First name does not appear to be")]
    public string FirstName {get; set;} 
    
    [Required(ErrorMessage = "Last name does not appear to be")]
    public string LastName {get; set;} 
    
    [Required(ErrorMessage = "Email does not appear to be")]
    [EmailAddress(ErrorMessage = "Email does not appear to be")]
    public string Email {get; set;}
    
    [Required(ErrorMessage = "Street does not appear to be")]
    public string Address {get; set;} 
    
    [Required(ErrorMessage = "Apartment does not appear to be")]
    public string Address2 {get; set;} 
    
    [Required(ErrorMessage = "City does not appear to be")]
    public string City {get; set;}
    
    [Required(ErrorMessage = "Region does not appear to be")]
    public string Region {get; set;} 
    public string PostCode {get; set;}
}