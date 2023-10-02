using ReturnTrue.AspNetCore.Identity.Anonymous;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseAnonymousId();
app.UseRouting();

app.MapControllerRoute(
    name: "ProductDetails",
    pattern: "{controller=Product}/{action=DetailsName}/{name}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();