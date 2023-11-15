using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models;

public class Role
{
    [Required(ErrorMessage = "Where is the role's name?")]
    public string RoleName { get; set; }
}