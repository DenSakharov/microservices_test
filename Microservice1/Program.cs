var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultControllerRoute();

//app.MapGet("/", () => "Hello World!");

app.Run();
