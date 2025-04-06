using AutoMapper;
using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;
using Identity.API.Exceptions;
using Identity.API.Interfaces;
using Identity.API.Models;
using Identity.API.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValidationException = Identity.API.Exceptions.ValidationException;

namespace Identity.API.Services
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRoleModel> _roleManager;
        private readonly IMapper _mapper;

        public ApplicationRoleService(RoleManager<ApplicationRoleModel> roleManager, IMapper mapper)
        {           
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetRolesAsync()
        {
            var rolesModel = await _roleManager.Roles.ToListAsync();

            return _mapper.Map<IEnumerable<RoleResponseDto>>(rolesModel);
        }
        
        public async Task<RoleResponseDto?> GetRoleByIdAsync(string roleId)
        {
            var roleModel = await _roleManager.FindByIdAsync(roleId);

            return _mapper.Map<RoleResponseDto>(roleModel);
        }

        public async Task<RoleResponseDto?> GetRoleByNameAsync(string roleName)
        {
            var roleModel = await _roleManager.FindByNameAsync(roleName);

            return _mapper.Map<RoleResponseDto>(roleModel);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> CreateRoleAsync(CreateApplicationRoleRequestDto role)
        {

            var rules = new CreateApplicationRoleRequestDtoValidator(this);

            var validationResult = await rules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var roleModel = _mapper.Map<ApplicationRoleModel>(role);

            var result = await _roleManager.CreateAsync(roleModel);

            return result.Succeeded;
            
        }

        public async Task<bool> UpdateRoleAsync(UpdateApplicationRoleRequestDto role)
        {
            var validationUpdateRules = new UpdateApplicationRoleRequestDtoValidator(this);

            var validationResult = await validationUpdateRules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }
            
            var roleDB = await _roleManager.FindByIdAsync(role.Id!);

            _mapper.Map(role, roleDB);

            var result = await _roleManager.UpdateAsync(roleDB!);

            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleModel = await _roleManager.FindByIdAsync(roleId);

            if (roleModel == null)
            {
                throw new ValidationException(new List<ErrorValidation>
                {
                    new ErrorValidation("RoleId", "Role not found")
                });
            }

            var result = await _roleManager.DeleteAsync(roleModel);
            
            return result.Succeeded;
        }
                
    }
}
