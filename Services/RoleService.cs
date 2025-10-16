using E_learning_platform.Dto.Requests;
using E_learning_platform.Models;
using E_learning_platform.Repositories;
namespace E_learning_platform.Services
{
    public class RoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public Task<List<Role>> GetAllRolesAsync()
        {
            return roleRepository.GetAllRolesAsync();
        }
        public Task<Role?> GetRoleByIdAsync(long id)
        {
            return roleRepository.GetRoleByIdAsync(id);
        }
        public Task CreateRoleAsync(RoleRequest roleRequest)
        {
            return roleRepository.CreateRoleAsync(roleRequest);
        }
        public Task UpdateRoleAsync(long id, RoleRequest roleRequest)
        {
            return roleRepository.UpdateRoleAsync(id, roleRequest);
        }
        public Task DeleteRoleAsync(long id)
        {
            return roleRepository.DeleteRoleAsync(id);
        }
    }
}
