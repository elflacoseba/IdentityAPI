using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;

namespace Identity.API.Interfaces
{
    public interface IApplicationUserService
    {
        #region Users
        
        Task<IEnumerable<UserResponseDto>> GetUsersAsync();
        Task<UserResponseDto?> GetUserByUsernameAsync(string username);
        Task<UserResponseDto?> GetUserByIdAsync(string userId);
        Task<UserResponseDto?> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(CreateApplicationUserRequestDto user);
        Task<bool> UpdateUserAsync(UpdateApplicationUserRequestDto user);
        Task<bool> DeleteUserAsync(string userId);

        #endregion

        #region SingIn

        Task<bool> CheckPasswordSignInAsync(string userId, string password, bool lockoutOnFailure);

        #endregion

        #region Roles

        Task<IList<string>> GetRolesAsync(string userId);
        Task<bool> AddToRoleAsync(string userId, string roleName);
        Task<bool> AddToRolesAsync(string userId, IEnumerable<string> roleNames);
        Task<bool> RemoveFromRoleAsync(string userId, string roleName);
        Task<bool> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames);

        #endregion
    }
}
