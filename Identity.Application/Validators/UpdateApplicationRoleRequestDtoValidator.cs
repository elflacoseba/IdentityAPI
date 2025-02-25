using FluentValidation;
using Identity.Application.Dtos.Request;
using Identity.Domain.Interfaces;

namespace Identity.Application.Validators
{
    public class UpdateApplicationRoleRequestDtoValidator : AbstractValidator<UpdateApplicationRoleRequestDto>
    {
        private readonly IApplicationRoleRepository _roleRepository;

        public UpdateApplicationRoleRequestDtoValidator(IApplicationRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(x => x.Name)
               .NotNull().WithMessage("El nombre del rol no puede ser nulo.")
               .NotEmpty().WithMessage("El nombre del rol no puede ser vacío.");

            When(Role => !string.IsNullOrEmpty(Role.Name), () =>
            {
                RuleFor(x => x.Name)
               .MustAsync(async (role, roleName, cancellationToken) =>
               {
                   var roleEntity = await _roleRepository.GetRoleByNameAsync(roleName!);
                   
                   if (roleEntity != null && roleEntity.Id != role.Id)
                   {
                       return false;
                   }

                   return true;
               }).WithMessage("Ya existe un rol con el mismo nombre");
            });
        }

    }
}
