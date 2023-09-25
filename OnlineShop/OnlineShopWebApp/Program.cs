using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "ProductDetailsByName",
    pattern: "{controller=Product}/{action=DetailsName}/{name}");
app.MapControllerRoute(
    name: "ProductDetailsById",
    pattern: "{controller=Product}/{action=DetailsId}/{id}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();