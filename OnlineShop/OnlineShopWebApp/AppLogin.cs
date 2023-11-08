using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace WebApplication1.Authentications;

public static class AppLogin
{
    public static string FirstName { get; set; }
    public static string LastName { get; set; }
    public static string Email { get; set; }
    public static string Picture { get; set; }

    public static Func<OAuthCreatingTicketContext, Task> OnCreatingTicket(string appName)
    {
        return async context =>
        {
            Email = context.Identity.FindFirst(ClaimTypes.Email).Value;
            switch (appName)
            {
                case "Github":
                    var name = context.Identity.FindFirst("urn:github:name").Value;
                    FirstName = name.Split(' ')[0];
                    LastName = name.Split(' ')[1];
                    var login = context.Identity.FindFirst(ClaimTypes.Name).Value;
                    Picture = $"https://github.com/{login}.png";
                    break;
                case "Google":
                    FirstName = context.Identity.FindFirst(ClaimTypes.GivenName).Value;
                    LastName = context.Identity.FindFirst(ClaimTypes.Surname)?.Value;
                    Picture = context.User.GetProperty("picture").GetString();
                    break;
                case "Yandex":
                    FirstName = context.Identity.FindFirst(ClaimTypes.GivenName).Value;
                    LastName = context.Identity.FindFirst(ClaimTypes.Surname)?.Value;
                    var pictureId = context.User.GetProperty("default_avatar_id").GetString();
                    Picture = $"https://avatars.yandex.net/get-yapic/{pictureId}/islands-retina-50";
                    break;
            }

            await Task.FromResult(true);
        };
    }
}