using Authorization.Basics.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Authorization.Basics.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//local DataBase
builder.Services.AddDbContext<ApplicationDbContext>(
    config => {
        config.UseInMemoryDatabase("MEMORY");
        }
    )
    .AddIdentity<ApplicationUser, ApplicationRole>(config =>
    {
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireUppercase = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequiredLength = 6;
    }
    )
    .AddEntityFrameworkStores<ApplicationDbContext>() ;

//builder.Services.AddAuthentication("Cookie").AddCookie("Cookie",
//    config => {
//        config.LoginPath = "/Admin/Login";
//        config.AccessDeniedPath = "/Home/AccessDenied";
//        }
//    );

builder.Services.ConfigureApplicationCookie(
    config =>
    {
        config.LoginPath = "/Admin/Login";
        config.AccessDeniedPath = "/Home/AccessDenied";
    }
    );

//Add roles 
builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("Administrator",
        builder => builder.RequireClaim(ClaimTypes.Role, "Administrator")
        );

        //different types roles in one
        options.AddPolicy("Manager",
            builder=> builder.RequireAssertion(x=>x.User.HasClaim(ClaimTypes.Role,"Manager" )
            || x.User.HasClaim(ClaimTypes.Role, "Administrator") )
            );
    }
    );
//builder.Services.AddAuthorization(
//    options => options.AddPolicy("Manager",
//    builder => builder.RequireClaim(ClaimTypes.Role, "Manager")
//    ));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    Databaseintializer.Init(scope.ServiceProvider);
}
//app.MapGet("/", () => "Hello World!");
//сначала этот
app.UseRouting();
//
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
public class Databaseintializer
{
    public static void Init(IServiceProvider scopeServiceProvider)
    {
        var userManager=scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
        var user = new ApplicationUser()
        {
            UserName = "User",
            LastName = "LastName",
            FirstName = "FirstName"
        };
        var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
        if (result.Succeeded)
        {
            userManager.AddClaimAsync(user,new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
        }
        //context.Users.Add(user);
        //context.SaveChanges();
    }
}