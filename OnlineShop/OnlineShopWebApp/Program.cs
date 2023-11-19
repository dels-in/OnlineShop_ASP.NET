using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using Serilog;
using OnlineShopWebApp;
using OnlineShopWebApp.Areas.Admin.Controllers;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Authentications;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationName", "Online Shop");
});

var connection = builder.Configuration.GetConnectionString("online_shop");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductStorage, ProductsDbStorage>();
builder.Services.AddTransient<IStorage<Cart, Product>, CartsDbStorage>();
builder.Services.AddTransient<IStorage<Comparison, Product>, ComparisonDbStorage>();
builder.Services.AddTransient<IStorage<Wishlist, Product>, WishlistDbStorage>();
builder.Services.AddTransient<IStorage<Order, UserInfo>, CheckoutDbStorage>();
builder.Services.AddTransient<IStorage<Library, Product>, LibraryDbStorage>();
builder.Services.AddTransient<IRoleStorage, RolesDbStorage>();
builder.Services.AddSingleton<IUserInfoStorage, InMemoryUserInfoStorage>();
builder.Services.AddSingleton<IAccountStorage, InMemoryAccountStorage>();
builder.Services.AddSingleton<IFileStorage, InMemoryFileStorage>();
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