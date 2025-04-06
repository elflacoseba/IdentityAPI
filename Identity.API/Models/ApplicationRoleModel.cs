using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models
{
    public class ApplicationRoleModel : IdentityRole
    {
        public string? Description { get; set; }
    }
}
