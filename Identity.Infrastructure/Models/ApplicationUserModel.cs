using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
