namespace Identity.API.Exceptions
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

    public class ValidationException : Exception
    {
        public List<ErrorValidation> ValidationErrors { get; set; }

        public ValidationException(List<ErrorValidation> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public ValidationException()
        {
            ValidationErrors = new List<ErrorValidation>();
        }
    }
}
