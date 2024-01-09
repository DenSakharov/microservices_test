using Microsoft.AspNetCore.Identity;

namespace Authorization.VK.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
