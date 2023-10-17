using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IStorage<Cart, Product>, InMemoryCartsStorage>();
builder.Services.AddSingleton<IProductStorage, InMemoryProductStorage>();
builder.Services.AddSingleton<IFileStorage, InMemoryFileStorage>();
builder.Services.AddSingleton<IStorage<Comparition, Product>, InMemoryComparitionStorage>();
builder.Services.AddSingleton<IStorage<Wishlist, Product>, InMemoryWishlistStorage>();
builder.Services.AddTransient<IStorage<Validation, Checkout>, InMemoryCheckoutStorage>();
builder.Services.AddTransient<IAccountStorage, InMemoryAccountStorage>();
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