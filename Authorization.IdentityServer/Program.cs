using Authorization.IdentityServer;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
                                    .AddInMemoryClients(Configuration.GetClients())
                                    .AddInMemoryApiResources(Configuration.GetApiResources())
                                    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
                                    .AddDeveloperSigningCredential();   

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
