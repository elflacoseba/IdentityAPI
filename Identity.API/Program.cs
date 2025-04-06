
using FluentValidation;
using Identity.API.Context;
using Identity.API.Interfaces;
using Identity.API.Models;
using Identity.API.Services;
using Identity.API.Settings;
using Identity.API.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.API
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddSingleton(configuration);

            var passwordSettings = new PasswordSettings();
            configuration.GetSection("PasswordSettings").Bind(passwordSettings);

            var lockoutSettings = new LockoutSettings();
            configuration.GetSection("LockoutSettings").Bind(lockoutSettings);

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("IdentityDB_Connection")));            

            builder.Services.Configure<IdentityOptions>(options =>
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


            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddScoped<IApplicationRoleService, ApplicationRoleService>();

            builder.Services.AddIdentity<ApplicationUserModel, ApplicationRoleModel>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
