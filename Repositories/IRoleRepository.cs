using E_learning_platform.Models;
using E_learning_platform.DTOs.Requests;

namespace E_learning_platform.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetRoleByIdAsync(long id);
        Task<Role> CreateRoleAsync(RoleRequest roleRequest);
        Task<Role> UpdateRoleAsync(long id, RoleRequest roleRequest);
        Task<bool> DeleteRoleAsync(long id);

        Task changeRoleAsync(long id, bool status);


    }
}
