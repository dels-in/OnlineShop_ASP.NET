using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models;

public class UserInfo
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Address { get; set; }
    
    public string Address2 { get; set; }

    public string City { get; set; }

    public string Region { get; set; }

    public string PostCode { get; set; }

    public bool IsChecked { get; set; }
}