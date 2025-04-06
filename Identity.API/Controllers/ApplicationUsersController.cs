using Microsoft.AspNetCore.Mvc;
using Identity.API.Interfaces;
using Identity.API.Dtos.Request;
using Identity.API.Models.Errors;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        public readonly IApplicationUserService _userService;

        public ApplicationUsersController(IApplicationUserService userApplication)
        {
            _userService = userApplication;
        }

        #region Users

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        [HttpGet]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateApplicationUserRequestDto user)
        {
            var result = await _userService.CreateUserAsync(user);
            
            if (result)
            {
                return Ok("Usuario creado correctamente.");
            }
            else
            {
                return BadRequest("Error al crear el usuario.");                
            }
        }

        [HttpPatch]
        [Route("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UpdateApplicationUserRequestDto user)
        {
            var userDB = await _userService.GetUserByIdAsync(user.Id!);

            if (userDB == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var result = await _userService.UpdateUserAsync(user);

            if (result)
            {
                return Ok("Usuario modificado correctamente.");
            }
            else
            {
                return BadRequest("Error al modificar el usuario.");
            }
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var result = await _userService.DeleteUserAsync(userId);

            if (result)
            {
                return Ok("Usuario eliminado correctamente.");
            }
            else
            {
                return BadRequest("Error al eliminar el usuario.");
            }
        }

        #endregion

        #region SingIn
        [HttpGet]
        [Route("CheckPasswordSignInAsync")]
        public async Task<IActionResult> CheckPasswordSignInAsync(string userId, string password, bool lockoutOnFailure)
        {
            var result = await _userService.CheckPasswordSignInAsync(userId, password, lockoutOnFailure);

            if (result)
            {
                return Ok("El password del usuario es válido");
            }
            else
            {
                return BadRequest("Credenciales incorrectas!");
            }
        }
        #endregion

        #region Roles

        [HttpGet]
        [Route("GetRolesAsync")]
        public async Task<IActionResult> GetRolesAsync(string userId)
        {
            var userRoles = await _userService.GetRolesAsync(userId);

            if (userRoles != null)
            {
                return Ok(userRoles);
            }
            else
            {
                return NotFound("El usuario no está asignado a ningún rol.");
            }
        }

        [HttpPost]
        [Route("AddToRoleAsync")]
        public async Task<IActionResult> AddToRoleAsync(string userId, string roleName)
        {
            var result = await _userService.AddToRoleAsync(userId, roleName);
            if (result)
            {
                return Ok("Rol asignado correctamente.");
            }
            else
            {
                return BadRequest("Error al asignar el rol.");
            }
        }

        [HttpPost]
        [Route("AddToRolesAsync")]
        public async Task<IActionResult> AddToRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            var result = await _userService.AddToRolesAsync(userId, roleNames);

            if (result)
            {
                return Ok("Roles asignados correctamente.");
            }
            else
            {
                return BadRequest("Error al asignar los roles.");
            }
        }

        [HttpPost]
        [Route("RemoveFromRoleAsync")]
        public async Task<IActionResult> RemoveFromRoleAsync(string userId, string roleName)
        {
            var result = await _userService.RemoveFromRoleAsync(userId, roleName);
            if (result)
            {
                return Ok("Rol removido correctamente.");
            }
            else
            {
                return BadRequest("Error al remover el rol.");
            }
        }

        [HttpPost]
        [Route("RemoveFromRolesAsync")]
        public async Task<IActionResult> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            var result = await _userService.RemoveFromRolesAsync(userId, roleNames);
            if (result)
            {
                return Ok("Roles removidos correctamente.");
            }
            else
            {
                return BadRequest("Error al remover los roles.");
            }
        }

        #endregion
    }
}
