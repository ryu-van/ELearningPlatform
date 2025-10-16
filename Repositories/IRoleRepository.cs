using E_learning_platform.Models;
using E_learning_platform.Dto.Requests;

namespace E_learning_platform.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(long id);
        Task CreateRoleAsync(RoleRequest roleRequest);
        Task UpdateRoleAsync(long id, RoleRequest roleRequest);
        Task DeleteRoleAsync(long id);
    }
}
