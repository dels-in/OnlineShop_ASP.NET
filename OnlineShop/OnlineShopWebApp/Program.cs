using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CartStorage>();
builder.Services.AddSingleton<ProductStorage>();
builder.Services.AddSingleton<FileStorage>();
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