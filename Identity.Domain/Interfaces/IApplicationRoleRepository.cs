using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    public interface IApplicationRoleRepository : IDisposable
    {
        Task<IEnumerable<ApplicationRole>?> GetRolesAsync();
        Task<ApplicationRole?> GetRoleByIdAsync(string roleId);
        Task<ApplicationRole?> GetRoleByNameAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<bool> CreateRoleAsync(ApplicationRole role);
        Task<bool> UpdateRoleAsync(ApplicationRole role);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}
