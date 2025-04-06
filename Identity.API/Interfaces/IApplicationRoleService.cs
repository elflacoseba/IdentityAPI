using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;

namespace Identity.API.Interfaces
{
    public interface IApplicationRoleService
    {
        Task<IEnumerable<RoleResponseDto>> GetRolesAsync();
        Task<RoleResponseDto?> GetRoleByIdAsync(string roleId);
        Task<RoleResponseDto?> GetRoleByNameAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<bool> CreateRoleAsync(CreateApplicationRoleRequestDto role);
        Task<bool> UpdateRoleAsync(UpdateApplicationRoleRequestDto role);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}
