using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace WebApplication1.Authentications;

public static class AppLogin
{
    public static string FirstName { get; set; }
    public static string LastName { get; set; }
    public static string Email { get; set; }

    public static Func<OAuthCreatingTicketContext, Task> OnCreatingTicket(string appName)
    {
        return async context =>
        {
            switch (appName)
            {
                case "Github":
                    var name = context.Identity.FindFirst("urn:github:name").Value;
                    FirstName = name.Split(' ')[0];
                    LastName = name.Split(' ')[1];
                    Email = context.Identity.FindFirst(ClaimTypes.Email).Value;
                    break;
                case "Google":
                case "Yandex":
                    FirstName = context.Identity.FindFirst(ClaimTypes.GivenName).Value;
                    LastName = context.Identity.FindFirst(ClaimTypes.Surname)?.Value;
                    Email = context.Identity.FindFirst(ClaimTypes.Email).Value;
                    break;
            }

            await Task.FromResult(true);
        };
    }
}