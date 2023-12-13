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
        if (roleManager.FindByNameAsync("Admin").Result == null)
        {
            roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        }

        if (roleManager.FindByNameAsync("User").Result == null)
        {
            roleManager.CreateAsync(new IdentityRole("User")).Wait();
        }

        if (userManager.FindByNameAsync(adminEmail).Result == null)
        {
            var admin = new User { UserName = adminEmail, Email = adminEmail, Password = password.Encrypt(), ConfirmPassword = password.Encrypt(), RoleName = "Admin" };
            userManager.CreateAsync(admin, password.Encrypt()).Wait();
        }
    }
}