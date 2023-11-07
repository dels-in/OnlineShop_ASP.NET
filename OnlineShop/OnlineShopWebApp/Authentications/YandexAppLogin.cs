using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace WebApplication1.Authentications;

public static class YandexAppLogin
{
    public static string FirstName { get; set; }
    public static string LastName { get; set; }
    public static string Email { get; set; }

    public static Func<OAuthCreatingTicketContext, Task> OnCreatingTicket()
    {
        return async context =>
        {
            var name = context.Identity.FindFirst(ClaimTypes.Name).Value;
            Email = context.Identity.FindFirst(ClaimTypes.Email).Value;


            await Task.FromResult(true);
        };
    }
}