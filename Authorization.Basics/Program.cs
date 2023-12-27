var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie").AddCookie("Cookie",config=>config.LoginPath="/Admin/Login");
builder.Services.AddAuthorization();

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
