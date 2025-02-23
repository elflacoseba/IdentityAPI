
using Identity.Application.Extensions;
using Identity.Infrastructure.Extensions;
using Identity.WebAPI.Middlewares;

namespace Identity.API
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddApplicationLayer(configuration);
            builder.Services.AddInfrastructureLayer(configuration);

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
