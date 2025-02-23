using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Models
{
    public class ApplicationRoleModel : IdentityRole
    {
        public string? Description { get; set; }
    }
}
