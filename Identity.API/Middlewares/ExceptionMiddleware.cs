using Identity.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Identity.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ValidationException ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(new { 
                message = "Hay errores de validación",
                errors = ex.ValidationErrors
            });

            response.StatusCode = StatusCode;
            await response.WriteAsync(result);
        }
    }
}
