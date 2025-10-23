using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IFeatureRepository featureRepository;
        private readonly IMapper mapper;

        public RoleService(IRoleRepository roleRepository, IFeatureRepository featureRepository, IMapper mapper)
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
            var role = await roleRepository.GetRoleByIdAsync(id);
            return role == null ? null : mapper.Map<RoleResponse>(role);
        }

        public async Task<RoleResponse> CreateRoleAsync(RoleRequest roleRequest)
        {
            if (string.IsNullOrWhiteSpace(roleRequest.Name))
            {
                throw new ArgumentException("Role name is required", nameof(roleRequest.Name));
            }
            Role createdRole = await roleRepository.CreateRoleAsync(roleRequest);
            return mapper.Map<RoleResponse>(createdRole);
        }

        public async Task<RoleResponse?> UpdateRoleAsync(long id, RoleRequest roleRequest)
        {
            var updatedRole = await roleRepository.UpdateRoleAsync(id, roleRequest);
            return updatedRole == null ? null : mapper.Map<RoleResponse>(updatedRole);
        }

        public async Task<bool> DeleteRoleAsync(long id)
        {
            var RoleExists = await roleRepository.GetRoleByIdAsync(id);
            if (RoleExists == null)
            {
                return false;
            }
            return await roleRepository.DeleteRoleAsync(id);
        }

        public async Task<IEnumerable<FeatureResponse>> GetAllFeaturesAsync()
        {
            var features = await featureRepository.GetAllAsync();
            return mapper.Map<IEnumerable<FeatureResponse>>(features);
        }

        public async Task<bool> ChangeFeatureAsync(long id, bool status)
        {
            return await featureRepository.changeStatusFeature(id, status);
        }

        public async Task<bool> changeRoleStatus(long id, bool status)
        {
          return await roleRepository.changeRoleAsync(id, status);

        }
    }
}
