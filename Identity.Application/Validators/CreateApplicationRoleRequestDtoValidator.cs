using FluentValidation;
using Identity.Application.Dtos.Request;
using Identity.Domain.Interfaces;

namespace Identity.Application.Validators
{
    public class CreateApplicationRoleRequestDtoValidator : AbstractValidator<CreateApplicationRoleRequestDto>
    {
        private readonly IApplicationRoleRepository _roleRepository;

        public CreateApplicationRoleRequestDtoValidator(IApplicationRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(x => x.Name)
               .NotNull().WithMessage("El nombre del rol no puede ser nulo.")
               .NotEmpty().WithMessage("El nombre del rol no puede ser vacío.");
               

            When(Role => !string.IsNullOrEmpty(Role.Name), () =>
            {
               RuleFor(x => x.Name)
               .MustAsync(RoleNameFree).WithMessage("Ya existe un rol con el mismo nombre");
            });
        }

        /// <summary>
        /// Verifica si el nombre del rol está libre. (No existe en la base de datos).
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> RoleNameFree(string? roleName, CancellationToken token)
        {
            if (string.IsNullOrEmpty(roleName))
                return false;

            var role = await _roleRepository.GetRoleByNameAsync(roleName);

            if (role is null)
                return true;
            else
                return false;

        }
    }
}
