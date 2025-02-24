using AutoMapper;
using FluentValidation;
using Identity.Application.Dtos.Request;
using Identity.Application.Dtos.Response;
using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using Identity.Application.Validators;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using ValidationException = Identity.Application.Exceptions.ValidationException;

namespace Identity.Application.Services
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IApplicationRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateApplicationRoleRequestDtoValidator validationUpdateRules;

        public ApplicationRoleService(IApplicationRoleRepository roleRepository, IMapper mapper, UpdateApplicationRoleRequestDtoValidator validationUpdateRules)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            this.validationUpdateRules = validationUpdateRules;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetRolesAsync()
        {
            var rolesEntity = await _roleRepository.GetRolesAsync();

            return _mapper.Map<IEnumerable<RoleResponseDto>>(rolesEntity);
        }
        
        public async Task<RoleResponseDto?> GetRoleByIdAsync(string roleId)
        {
            var roleEntity = await _roleRepository.GetRoleByIdAsync(roleId);

            return _mapper.Map<RoleResponseDto>(roleEntity);
        }

        public async Task<RoleResponseDto?> GetRoleByNameAsync(string roleName)
        {
            var roleEntity = await _roleRepository.GetRoleByNameAsync(roleName);

            return _mapper.Map<RoleResponseDto>(roleEntity);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleRepository.RoleExistsAsync(roleName);
        }

        public async Task<bool> CreateRoleAsync(CreateApplicationRoleRequestDto role)
        {
            
            var rules = new CreateApplicationRoleRequestDtoValidator(_roleRepository);

            var validationResult = await rules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el username en la base de datos
            var roleName = await _roleRepository.GetRoleByNameAsync(role.Name!);

            if (roleName != null)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(roleName.Name), "El nombre del rol ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            var roleEntity = _mapper.Map<ApplicationRole>(role);

            return await _roleRepository.CreateRoleAsync(roleEntity);
        }

        public async Task<bool> UpdateRoleAsync(UpdateApplicationRoleRequestDto role)
        {
            var validationResult = await validationUpdateRules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el username en la base de datos
            var roleName = await _roleRepository.GetRoleByNameAsync(role.Name!);

            if (roleName != null && roleName.Id != role.Id)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(roleName.Name), "El nombre del rol ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            roleName = _mapper.Map<ApplicationRole>(role);
        
            return await _roleRepository.UpdateRoleAsync(roleName);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            return await _roleRepository.DeleteRoleAsync(roleId);
        }
                
    }
}
