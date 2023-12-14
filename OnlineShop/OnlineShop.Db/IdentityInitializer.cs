using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class IdentityInitializer
{
    public static void Initialize(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        var adminEmail = "admin@mail.ru";
        var password = "Admin123";
        var adminRole = new Role { Name = "Admin" };
        var userRole = new Role { Name = "User" };
        
        if (roleManager.FindByNameAsync("Admin").Result == null)
        {
            roleManager.CreateAsync(adminRole).Wait();
        }

        if (roleManager.FindByNameAsync("User").Result == null)
        {
            roleManager.CreateAsync(userRole).Wait();
        }

        if (userManager.FindByNameAsync(adminEmail).Result == null)
        {
            var admin = new User
            {
                UserName = adminEmail, Email = adminEmail, Password = password.Encrypt(),
                ConfirmPassword = password.Encrypt(), Roles = new List<Role> { adminRole, userRole }
            };
            userManager.CreateAsync(admin, password.Encrypt()).Wait();
        }
    }
}