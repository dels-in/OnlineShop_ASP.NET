using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Models;

public class Checkout
{
    public string FirstName {get; set;} 
    public string LastName {get; set;} 
    public string Email {get; set;}
    public string Address {get; set;} 
    public string Address2 {get; set;} 
    public string City {get; set;}
    public string Region {get; set;} 
    public string PostCode {get; set;}
}