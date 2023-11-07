using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace WebApplication1.Authentications;

public static class GithubAppLogin
{
    public static string FirstName { get; set; }
    public static string LastName { get; set; }
    public static string Email { get; set; }

    public static Func<OAuthCreatingTicketContext, Task> OnCreatingTicket()
    {
        return async context =>
        {
            var name = context.Identity.FindFirst("urn:github:name").Value;
            FirstName = name.Split(' ')[0];
            LastName = name.Split(' ')[1];
            Email = context.Identity.FindFirst(ClaimTypes.Email).Value;
            
            await Task.FromResult(true);
        };
    }
}