using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using Serilog;
using WebApplication1;
using WebApplication1.Areas.Admin.Controllers;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Authentications;
using WebApplication1.Models;
using WebApplication1.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationName", "Online Shop");
});

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IStorage<Cart, Product>, InMemoryCartsStorage>();
builder.Services.AddSingleton<IProductStorage, InMemoryProductStorage>();
builder.Services.AddSingleton<IFileStorage, InMemoryFileStorage>();
builder.Services.AddSingleton<IStorage<Comparison, Product>, InMemoryComparisonStorage>();
builder.Services.AddSingleton<IStorage<Wishlist, Product>, InMemoryWishlistStorage>();
builder.Services.AddSingleton<IStorage<Order, UserInfo>, InMemoryCheckoutStorage>();
builder.Services.AddSingleton<IStorage<Library, Product>, InMemoryLibraryStorage>();
builder.Services.AddSingleton<IRoleStorage, InMemoryRoleStorage>();
builder.Services.AddSingleton<IAccountStorage, InMemoryAccountStorage>();
builder.Services.AddSingleton<IUserInfoStorage, InMemoryUserInfoStorage>();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Home/Index";
        options.SlidingExpiration = true;
    })
    .AddGitHub("Github", options =>
    {
        options.ClientId = TokenStorage.GetClientInfo("tokens/github.txt", true);
        options.ClientSecret = TokenStorage.GetClientInfo("tokens/github.txt", false);
        options.Scope.Add("read:user");
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = AppLogin.OnCreatingTicket("Github")
        };
    })
    .AddGoogle("Google", options =>
    {
        options.ClientId = TokenStorage.GetClientInfo("tokens/google.txt", true);
        options.ClientSecret = TokenStorage.GetClientInfo("tokens/google.txt", false);
        options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = AppLogin.OnCreatingTicket("Google")
        };
    })
    .AddYandex("Yandex", options =>
    {
        options.ClientId = TokenStorage.GetClientInfo("tokens/yandex.txt", true);
        options.ClientSecret = TokenStorage.GetClientInfo("tokens/yandex.txt", false);
        options.CallbackPath = "/yandex-signin";
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = AppLogin.OnCreatingTicket("Yandex")
        };
    })
    .AddVkontakte("Vkontakte", options =>
    {
        options.ClientId = TokenStorage.GetClientInfo("tokens/vkontakte.txt", true);
        options.ClientSecret = TokenStorage.GetClientInfo("tokens/vkontakte.txt", false);
        options.Scope.Add("email");
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = AppLogin.OnCreatingTicket("Vkontakte")
        };
    });

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAnonymousId();
app.UseRouting();
app.UseSerilogRequestLogging();
app.UseAuthorization();
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();