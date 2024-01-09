using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Authorization.VK.Controllers
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
        public IEnumerable<AuthenticationScheme> ExternalProviders { get; internal set; }
    }
}
