using Identity.API.Models.Errors;
using Identity.API.Exceptions;
using System.Net;
using System.Text.Json;

namespace Identity.API.Middlewares
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

            var errorValidationResponse = new ValidationErrorResponse();

            errorValidationResponse.Message = "Hay errores de validación";

            var errors = ex.ValidationErrors.Select(error => new API.Models.Errors.ErrorValidation
            {
                PropertyName = error.PropertyName,
                ErrorMessage = error.ErrorMessage
            }).ToList();

            errorValidationResponse.Errors?.AddRange(errors);

            var StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(errorValidationResponse);

            response.StatusCode = StatusCode;
            await response.WriteAsync(result);
        }
    }
}
