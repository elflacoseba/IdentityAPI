using Microsoft.AspNetCore.Mvc;
using Identity.API.Interfaces;
using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationRolesController : ControllerBase
    {
        public readonly IApplicationRoleService _roleService;

        public ApplicationRolesController(IApplicationRoleService roleApplication)
        {
            _roleService = roleApplication;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> Index()
        {
            var roles = await _roleService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet("GetRoleById/{roleId}")]
        public async Task<ActionResult<RoleResponseDto>> GetRoleById(string roleId)
        {
            var user = await _roleService.GetRoleByIdAsync(roleId);

            if (user is null)
            {
                return NotFound("Rol no encontrado.");
            }
            
            return Ok(user);
        }

        [HttpGet("GetRoleByName/{roleName}")]
        
        public async Task<ActionResult<RoleResponseDto>> GetRoleByName(string roleName)
        {
            var user = await _roleService.GetRoleByNameAsync(roleName);

            if (user is null)
            {
                return NotFound("Rol no encontrado.");
            }

            return Ok(user);
        }

        [HttpPost("RoleExists/{roleName}")]
        public async Task<ActionResult> RoleExists(string roleName)
        {
            var result = await _roleService.RoleExistsAsync(roleName);

            if (result)
            {
                return Ok("El rol ya existe.");
            }
            else
            {
                return NotFound("El rol no existe.");
            }            
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateApplicationRoleRequestDto role)
        {
            var roleCreated = await _roleService.CreateRoleAsync(role);
            
            if (roleCreated is null)
            {                
                return BadRequest("Error al crear el rol.");
            }

            return CreatedAtRoute("GetRoleById", new { roleId = roleCreated.Id }, roleCreated );
        }

        [HttpPut("UpdateRole/{roleId}")]        
        public async Task<ActionResult> UpdateRole(string roleId, CreateApplicationRoleRequestDto role)
        {
            var userDB = await _roleService.GetRoleByIdAsync(roleId);

            if (userDB == null)
            {
                return NotFound("Rol no encontrado.");
            }

            var updateRoleDto = new UpdateApplicationRoleRequestDto
            {
                Id = roleId,
                Name = role.Name,
                Description = role.Description
            };


            var result = await _roleService.UpdateRoleAsync(updateRoleDto);

            if (!result)
            {            
                return BadRequest("Error al modificar el rol.");
            }
            
            return NoContent();
        }

        [HttpPost]
        [Route("DeleteRole")]
        public async Task<ActionResult> DeleteRole(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);

            if (role == null)
            {
                return NotFound("Rol no encontrado.");
            }

            var result = await _roleService.DeleteRoleAsync(roleId);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al eliminar el rol.");
            }
        }
    }
}
