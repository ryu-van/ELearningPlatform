using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllRolesAsync();
        Task<RoleResponse?> GetRoleByIdAsync(long id);
        Task<RoleResponse> CreateRoleAsync(RoleRequest roleRequest);
        Task<RoleResponse?> UpdateRoleAsync(long id, RoleRequest roleRequest);
        Task<bool> DeleteRoleAsync(long id);
        Task<IEnumerable<FeatureResponse>> GetAllFeaturesAsync();
        Task<bool> ChangeFeatureAsync(long id, bool status);
    }
}
