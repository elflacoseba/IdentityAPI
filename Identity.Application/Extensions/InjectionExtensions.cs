using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Application.Interfaces;
using System.Reflection;
using Identity.Application.Services;
using FluentValidation;

namespace Identity.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();

            return services;
        }
    }
}
