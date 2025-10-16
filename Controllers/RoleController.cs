using Microsoft.AspNetCore.Mvc;
using E_learning_platform.Services;
namespace E_learning_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController
    {
        private readonly RoleService roleService;
        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleService.GetAllRolesAsync();
            return new OkObjectResult(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(long id)
        {
            var role = await roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] E_learning_platform.Dto.Requests.RoleRequest roleRequest)
        {
            await roleService.CreateRoleAsync(roleRequest);
            return new OkResult();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(long id, [FromBody] E_learning_platform.Dto.Requests.RoleRequest roleRequest)
        {
            var existingRole = await roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                return new NotFoundResult();
            }
            await roleService.UpdateRoleAsync(id, roleRequest);
            return new OkResult();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var existingRole = await roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                return new NotFoundResult();
            }
            await roleService.DeleteRoleAsync(id);
            return new OkResult();
        }

        [HttpGet("features")]
        public async Task<IActionResult> GetAllFeatures()
        {
            var features = await roleService.GetAllFeaturesAsync();
            return new OkObjectResult(features);
        }
        [HttpPut("features/{id}/disable")]
        public async Task<IActionResult> DisableFeature(long id)
        {
            await roleService.DisableFeature(id);
            return new OkResult();
        }
    }
}
