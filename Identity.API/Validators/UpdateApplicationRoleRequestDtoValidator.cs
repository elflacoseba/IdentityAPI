using FluentValidation;
using Identity.API.Dtos.Request;
using Identity.API.Interfaces;

namespace Identity.API.Validators
{
    public class UpdateApplicationRoleRequestDtoValidator : AbstractValidator<UpdateApplicationRoleRequestDto>
    {
        private readonly IApplicationRoleService _roleService;

        public UpdateApplicationRoleRequestDtoValidator(IApplicationRoleService roleService)
        {
            _roleService = roleService;
            
            RuleFor(x => x.Name)
               .NotNull().WithMessage("El nombre del rol no puede ser nulo.")
               .NotEmpty().WithMessage("El nombre del rol no puede ser vacío.");

            When(Role => !string.IsNullOrEmpty(Role.Name), () =>
            {
                RuleFor(x => x.Name)
               .MustAsync(async (role, roleName, cancellationToken) =>
               {
                   var roleEntity = await _roleService.GetRoleByNameAsync(roleName!);

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
