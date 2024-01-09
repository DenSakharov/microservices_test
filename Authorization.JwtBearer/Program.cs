using Authorization.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("OAuth").
    AddJwtBearer("OAuth", config =>
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);

        config.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnMessageReceived = context =>
                {
                    if (context.Request.Query.ContainsKey("t"))
                    {
                        context.Token = context.Request.Query["t"];
                    }
                    return Task.CompletedTask;
                }
        };
        config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidIssuer = Constants.Issuer,
            ValidAudience = Constants.Audience,
            IssuerSigningKey = key
        };
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
