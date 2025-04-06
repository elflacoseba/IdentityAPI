using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity.API.Models;

namespace Identity.API.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUserModel> ApplicationUsers { get; set; }
        public DbSet<ApplicationRoleModel> ApplicationRoles { get; set; }
    }
}
