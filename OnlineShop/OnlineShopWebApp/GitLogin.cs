using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

public static class GitLogin
{
    public static string Name { get; set; }
    public static string Email { get; set; }

    public static Func<OAuthCreatingTicketContext, Task> OnCreatingGitHubTicket()
    {
        return async context =>
        {
            Name = context.Identity.FindFirst("urn:github:name").Value;
            Email = context.Identity.FindFirst(ClaimTypes.Email).Value;
            
            await Task.FromResult(true);
        };
    }
}