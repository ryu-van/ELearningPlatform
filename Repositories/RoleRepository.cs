using E_learning_platform.Data;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
using E_learning_platform.Dto.Requests;
namespace E_learning_platform.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await applicationDbContext.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Feature)
                .ToListAsync();
        } 
        public async Task<Role?> GetRoleByIdAsync(long id)
        {

            return await applicationDbContext.Roles.Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Feature)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateRoleAsync(RoleRequest roleRequest)
        {
            var newRole = new Role
            {
                Name = roleRequest.Name,
                Description = roleRequest.Description,
                IsActive = roleRequest.IsActive,
            };
            foreach (var featureId in roleRequest.FeatureIds)
            {
                newRole.RolePermissions.Add(new RolePermission
                {
                    FeatureId = featureId,
                    IsEnabled = true
                });
            }

            applicationDbContext.Roles.Add(newRole);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(long roleId, RoleRequest roleRequest)
        {
            var existingRole = await applicationDbContext.Roles.Include(r=> r.RolePermissions).FirstOrDefaultAsync(r=> r.Id == roleId);
            if (existingRole == null)
            {
                throw new Exception("Role not found");
            }
            existingRole.Name = roleRequest.Name;
            existingRole.Description = roleRequest.Description;
            var currentFeatureIds = existingRole.RolePermissions.Select(rp => rp.FeatureId).ToList();
            var newFeatureIds = roleRequest.FeatureIds.Distinct().ToList();
            var toRemove = existingRole.RolePermissions.Where(rp => !newFeatureIds.Contains(rp.FeatureId)).ToList();
            applicationDbContext.RemoveRange(toRemove);
            var toAdd = newFeatureIds.Where(id => !currentFeatureIds.Contains(id)).Select(id=> new RolePermission { RoleId = roleId,FeatureId = id, IsEnabled = true}).ToList();
            applicationDbContext.AddRange(toAdd);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(long id)
        {
            var role = await applicationDbContext.Roles.FindAsync(id);
            if (role != null)
            {
                applicationDbContext.Roles.Remove(role);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task disableRoleAsync(long id)
        {
            var role = await applicationDbContext.Roles.FindAsync(id);
            if (role != null)
            {
                role.IsActive = false;
                await applicationDbContext.SaveChangesAsync();
            }
        }




    }
}
