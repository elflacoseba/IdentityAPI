using FluentValidation;
using Identity.Application.Dtos.Request;

namespace Identity.Application.Validators
{
    public class UpdateApplicationRoleRequestDtoValidator : AbstractValidator<UpdateApplicationRoleRequestDto>
    {
        public UpdateApplicationRoleRequestDtoValidator()
        {

            RuleFor(x => x.Name)
               .NotNull().WithMessage("El nombre del rol no puede ser nulo.")
               .NotEmpty().WithMessage("El nombre del rol no puede ser vacío.");        
        }        
    }
}
