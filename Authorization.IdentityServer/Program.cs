using Authorization.IdentityServer;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Добавление логгирования в консоль
builder.Logging.AddConsole();

builder.Services.AddIdentityServer()
                                    .AddInMemoryClients(Configuration.GetClients())
                                    .AddInMemoryApiResources(Configuration.GetApiResources())
                                    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
                                    //.AddInMemoryApiScopes(Configuration.GetApiScopes())
                                    .AddDeveloperSigningCredential();
// Добавление логгирования для IdentityServer
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
