var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("OAuth").
    AddJwtBearer("OAuth", config =>
    {

    });

builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    endpoints.MapDefaultControllerRoute()
    ); ;
app.Run();
