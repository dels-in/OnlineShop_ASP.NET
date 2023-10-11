using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICartsStorage, InMemoryCartsStorage>();
builder.Services.AddSingleton<IProductStorage, InMemoryProductStorage>();
builder.Services.AddSingleton<IFileStorage, InMemoryFileStorage>();
builder.Services.AddSingleton<IComparitionStorage, InMemoryComparitionStorage>();
builder.Services.AddSingleton<IWishlistStorage, InMemoryWishlistStorage>();
builder.Services.AddTransient<IPersonalDataStorage, InMemoryPersonalDataStorage>();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});

var app = builder.Build();

app.UseStaticFiles();
app.UseAnonymousId();
app.UseRouting();

app.MapControllerRoute(
    name: "ProductDetails",
    pattern: "{controller=Product}/{action=DetailsName}/{productName}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();