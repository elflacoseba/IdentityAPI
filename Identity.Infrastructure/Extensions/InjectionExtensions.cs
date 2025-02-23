using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Infrastructure.Persistence.Context;
using Identity.Infrastructure.Persistence.Repositories;
using Identity.Infrastructure.Models;
using System.Reflection;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Settings;

namespace Identity.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var passwordSettings = new PasswordSettings();
            configuration.GetSection("PasswordSettings").Bind(passwordSettings);

            var lockoutSettings = new LockoutSettings();
            configuration.GetSection("LockoutSettings").Bind(lockoutSettings);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("IdentityDB_Connection")));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<IdentityOptions>(options =>
            {                 // Password settings
                options.Password.RequireDigit = passwordSettings.RequireDigit;
                options.Password.RequiredLength = passwordSettings.RequiredLength;
                options.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = passwordSettings.RequireUppercase;
                options.Password.RequireLowercase = passwordSettings.RequireLowercase;
                options.Password.RequiredUniqueChars = passwordSettings.RequiredUniqueChars;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettings.DefaultLockoutTimeSpan);
                options.Lockout.MaxFailedAccessAttempts = lockoutSettings.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = lockoutSettings.AllowedForNewUsers;
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddIdentity<ApplicationUserModel, ApplicationRoleModel>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            return services;
        }
    }
}
