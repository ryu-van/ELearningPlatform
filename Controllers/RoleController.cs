using E_learning_platform.DTOs.Responses;
using E_learning_platform.Services;
using Microsoft.AspNetCore.Mvc;
using E_learning_platform.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;

namespace E_learning_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdmin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

       
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleResponse>> GetRoleById(long id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound(new { message = $"Role với ID {id} không tồn tại" });
            }

            return Ok(role);
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoleResponse>> CreateRole([FromBody] RoleRequest roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRole = await _roleService.CreateRoleAsync(roleRequest);

            return CreatedAtAction(
                nameof(GetRoleById),
                new { id = createdRole.Id },
                createdRole
            );
        }

       
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoleResponse>> UpdateRole(long id, [FromBody] RoleRequest roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRole = await _roleService.UpdateRoleAsync(id, roleRequest);

            if (updatedRole == null)
            {
                return NotFound(new { message = $"Role với ID {id} không tồn tại" });
            }

            return Ok(updatedRole);
        }

        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var result = await _roleService.DeleteRoleAsync(id);

            if (!result)
            {
                return NotFound(new { message = $"Role với ID {id} không tồn tại" });
            }

            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeRoleStatus(long id, [FromQuery] bool isActive)
        {
            bool result = await _roleService.changeRoleStatus(id, isActive);
            if (!result)
            {
                return NotFound(new { message = $"Role với ID {id} không tồn tại" });
            }

            string statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Role đã được {statusText}" });
        }



        [HttpGet("features")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FeatureResponse>>> GetAllFeatures()
        {
            var features = await _roleService.GetAllFeaturesAsync();
            return Ok(features);
        }

        [HttpPut("features/{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeFeatureStatus(long id, [FromQuery] bool isEnabled)
        {
            bool result = await _roleService.ChangeFeatureAsync(id, isEnabled);

            if (!result)
            {
                return NotFound(new { message = $"Feature với ID {id} không tồn tại" });
            }

            string statusText = isEnabled ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Feature đã được {statusText}" });
        }
    }
}
