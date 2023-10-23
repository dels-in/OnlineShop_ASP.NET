using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Role
{
    private static int _counter;
    public int RoleId { get; }

    [Required(ErrorMessage = "Where is the role's name?")]
    public string RoleName { get; set; }

    public Role(string name)
    {
        _counter++;
        RoleId = _counter;
        RoleName = name;
    }
}