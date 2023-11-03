using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using Serilog;
using WebApplication1.Areas.Admin.Controllers;
using WebApplication1.Areas.Admin.Models;
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
builder.Services.AddSingleton<IStorage<Comparition, Product>, InMemoryComparitionStorage>();
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

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAnonymousId();
app.UseRouting();
app.UseSerilogRequestLogging();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();