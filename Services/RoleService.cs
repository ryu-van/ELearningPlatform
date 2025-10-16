using AutoMapper;
using E_learning_platform.Dto.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using E_learning_platform.Repositories;
namespace E_learning_platform.Services
{
    public class RoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly FeatureRepository featureRepository;
        private readonly IMapper mapper;

        public RoleService(IRoleRepository roleRepository,FeatureRepository featureRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.featureRepository = featureRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
        {
            var roles = await roleRepository.GetAllAsync();
            return mapper.Map<IEnumerable<RoleResponse>>(roles);
        }
        public async Task<RoleResponse?> GetRoleByIdAsync(long id)
        {
            var role = roleRepository.GetRoleByIdAsync(id);
            return role == null ? null : mapper.Map<RoleResponse>(role);
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
        public async Task<IEnumerable<FeatureResponse>> GetAllFeaturesAsync()
        {
            var features = await featureRepository.GetAllAsync();
            return mapper.Map<IEnumerable<FeatureResponse>>(features);
        }
        public Task DisableFeature(long id)
        {
            return featureRepository.disableFeature(id);
        }
    }
}
