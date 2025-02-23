using FluentValidation;
using Identity.Application.Dtos.Request;

namespace Identity.Application.Validators
{
    public class ApplicationUserRequestDtoValidator : AbstractValidator<CreateApplicationUserRequestDto>
    {
        public ApplicationUserRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage("El nombre de usuario no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre de usuario no puede ser vacío.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("La contraseña no puede ser nula.")
                .NotEmpty().WithMessage("La contraseña no puede ser vacía.")
                .MinimumLength(8).WithMessage("La contraseña debe contener al menos {MinLength} caracteres.");

            RuleFor(x => x.Email)
               .NotNull().WithMessage("El e-mail no puede ser nulo.")
               .NotEmpty().WithMessage("El e-mail no puede estar vacío.")
               .EmailAddress().WithMessage("El texto no tiene el formato válido de una dirección de correo electrónico.")
               .MaximumLength(50).WithMessage("El e-mail puede contener hasta {MaxLength} caracteres como máximo.");

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("El nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50).WithMessage("El nombre puede contener hasta {MaxLength} caracteres como máximo.");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("El apellido no puede ser nulo.")
                .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                .MaximumLength(50).WithMessage("El apellido puede contener hasta {MaxLength} caracteres como máximo.");
        }
    }
}
