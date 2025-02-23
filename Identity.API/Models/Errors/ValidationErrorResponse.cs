namespace Identity.API.Models.Errors
{
    public class ErrorValidation
    {
        /// <summary>
        /// The name of the property.
        /// </summary>
        public string? PropertyName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string? ErrorMessage { get; set; }

        public ErrorValidation()
        {

        }

        public ErrorValidation(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }

    public class ValidationErrorResponse
    {
        public string? Message { get; set; }
        public List<ErrorValidation>? Errors { get; set; }

        public ValidationErrorResponse()
        {
            Message = string.Empty;
            Errors = new List<ErrorValidation>();
        }

        public ValidationErrorResponse(string? message, List<ErrorValidation>? errors)
        {
            Message = message;
            Errors = errors;
        }
    }
}
