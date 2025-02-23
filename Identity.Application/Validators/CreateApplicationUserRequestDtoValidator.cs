using FluentValidation;
using Identity.Application.Dtos.Request;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Validators
{
    public class CreateApplicationUserRequestDtoValidator : AbstractValidator<CreateApplicationUserRequestDto>
    {
        private readonly IConfiguration _configuration;

        public CreateApplicationUserRequestDtoValidator(IConfiguration configuration)
        {
            _configuration = configuration;

            var passwordSetting = configuration.GetSection("PasswordSettings");
            var passwordMinimunLength = passwordSetting.GetValue<int>("RequiredLength");
            var requiredUniqueChars = passwordSetting.GetValue<int>("RequiredUniqueChars");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("El nombre de usuario no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre de usuario no puede ser vacío.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("La contraseña no puede ser nula.")
                .NotEmpty().WithMessage("La contraseña no puede ser vacía.")              
                .Must(PasswordContainUpperCase).WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Must(PasswordContainLowerCase).WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Must(PasswordContainDigit).WithMessage("La contraseña debe contener al menos un dígito numérico.")
                .Must(PasswordContainNonAlphanumeric).WithMessage("La contraseña debe contener al menos un caracter especial.")            
                .Must(PasswordContainDistinctCharacters)
                .WithMessage($"La contraseña debe contener al menos {requiredUniqueChars} caracteres distintos.")
                .WithState(_ => new { CantidadDeCaracteres = requiredUniqueChars })
                .MinimumLength(passwordMinimunLength).WithMessage("La contraseña debe contener al menos {MinLength} caracteres.")
                .MaximumLength(16).WithMessage("La contraseña puede contener hasta {MaxLength} caracteres como máximo."); 

            RuleFor(x => x.Email)
               .NotNull().WithMessage("El email no puede ser nulo.")
               .NotEmpty().WithMessage("El email no puede estar vacío.")
               .EmailAddress().WithMessage("El texto no tiene el formato válido de una dirección de correo electrónico.")
               .MaximumLength(50).WithMessage("El email puede contener hasta {MaxLength} caracteres como máximo.");

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("El nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50).WithMessage("El nombre puede contener hasta {MaxLength} caracteres como máximo.");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("El apellido no puede ser nulo.")
                .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                .MaximumLength(50).WithMessage("El apellido puede contener hasta {MaxLength} caracteres como máximo.");
        }

        #region Validaciones Password

        private bool PasswordContainUpperCase(string? password)
        {
            if (password == null)
            {
                return false;
            }

            var requireUppercase = _configuration.GetValue<bool>("PasswordSettings:RequireUppercase");
            
            if (!requireUppercase)
            {
                return true;
            }

            return password!.Any(char.IsUpper);
        }

        private bool PasswordContainLowerCase(string? password)
        {
            if (password == null)
            {
                return false;
            }

            var requireLowercase = _configuration.GetValue<bool>("PasswordSettings:RequireLowercase");

            if (!requireLowercase)
            {
                return true;
            }

            return password!.Any(char.IsLower);
        }

        private bool PasswordContainDigit(string? password)
        {
            if (password == null)
            {
                return false;
            }

            var requireDigit = _configuration.GetValue<bool>("PasswordSettings:RequireDigit");

            if (!requireDigit)
            {
                return true;
            }

            return password.Any(char.IsDigit);
        }

        private bool PasswordContainNonAlphanumeric(string? password)
        {
            if (password == null)
            {
                return false;
            }

            var requireNonAlphanumeric = _configuration.GetValue<bool>("PasswordSettings:RequireNonAlphanumeric");

            if (!requireNonAlphanumeric)
            {
                return true;
            }

            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private bool PasswordContainDistinctCharacters(string? password)
        {
            if (password == null)
            {
                return false;
            }

            var requiredUniqueChars = _configuration.GetValue<int>("PasswordSettings:RequiredUniqueChars");

            return password.Distinct().Count() >= requiredUniqueChars;
        }

        #endregion

    }
}
