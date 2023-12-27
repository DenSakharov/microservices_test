using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie").AddCookie("Cookie",
    config => {
        config.LoginPath = "/Admin/Login";
        config.AccessDeniedPath = "/Home/AccessDenied";
        }
    );

//Add roles 
builder.Services.AddAuthorization(
    options => options.AddPolicy("Administrator",
    builder => builder.RequireClaim(ClaimTypes.Role, "Administrator")
    ));
builder.Services.AddAuthorization(
    options => options.AddPolicy("Manager",
    builder => builder.RequireClaim(ClaimTypes.Role, "Manager")
    ));

builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//сначала этот
app.UseRouting();
//
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
