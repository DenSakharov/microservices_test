using Authorization.VK.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authorization.VK.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        public AdminController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "Administrator")]
        public IActionResult Administrator()
        {
            return View();
        }
        [Authorize(Policy = "Manager")]
        public IActionResult Manager()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }
        [AllowAnonymous]
        public  IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback),"Admin",new {returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        public async Task< IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction("Login");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
            if (result.Succeeded)
            {

            }
            return RedirectToAction("RegisterExternal",new ExternalLoginViewModel
            {
                ReturnUrl= returnUrl,
                UserName=info.Principal.FindFirstValue(ClaimTypes.Name)
            });
        }
        public IActionResult RegisterExternal(ExternalLoginViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            //var user = await _context.Users.SingleOrDefaultAsync(
            //    x => x.UserName == model.UserName && x.Password == model.Password
            //    );
            var user=await _userManager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                ModelState.AddModelError("","User not found");
                return View(model);
            }

            var result= await _signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
            return View(model);
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, model.UserName),
            //    new Claim(ClaimTypes.Role,"Administrator")

            //};
            //var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            //var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            //await HttpContext.SignInAsync(claimPrincipal);

        }
        public async Task<IActionResult> LogOffAsync()
        {
            //HttpContext.SignOutAsync("Cookie");
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
