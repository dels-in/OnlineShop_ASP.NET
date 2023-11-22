using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models;

public class RoleViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Where is the role's name?")]
    public string RoleName { get; set; }
}