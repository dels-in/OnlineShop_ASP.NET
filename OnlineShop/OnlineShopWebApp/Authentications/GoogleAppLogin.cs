using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace WebApplication1.Authentications;

public static class GoogleAppLogin
{
    public static string FirstName { get; set; }
    public static string LastName { get; set; }
    public static string Email { get; set; }

    public static Func<OAuthCreatingTicketContext, Task> OnCreatingTicket()
    {
        return async context =>
        {
            FirstName = context.Identity.FindFirst(ClaimTypes.GivenName).Value;
            LastName = context.Identity.FindFirst(ClaimTypes.Surname)?.Value;
            Email = context.Identity.FindFirst(ClaimTypes.Email).Value;

            await Task.FromResult(true);
        };
    }
}