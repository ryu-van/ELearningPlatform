using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.Exceptions;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
namespace E_learning_platform.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Feature)
                .ToListAsync();
        } 
        public async Task<Role?> GetRoleByIdAsync(long id)
        {

            return await _context.Roles.Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Feature)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> CreateRoleAsync(RoleRequest roleRequest)
        {
            var existingFeatures = await _context.Features
                .Where(f=>roleRequest.FeatureIds.Contains(f.Id))
                .Select(f=>f.Id)
                .ToListAsync();
            var invalidIds = roleRequest.FeatureIds.Except(existingFeatures).ToList();
            if (invalidIds.Any())
            {
                throw new EntityNotFoundException("Feature", invalidIds);
            }

            var newRole = new Role
            {
                Name = roleRequest.Name,
                Description = roleRequest.Description,
                IsActive = roleRequest.IsActive,
                RolePermissions = roleRequest.FeatureIds.Select(featureId => new RolePermission
                {
                    FeatureId = featureId,
                    IsEnabled = true
                }).ToList()
            };
           

            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }

        public async Task<Role> UpdateRoleAsync(long roleId, RoleRequest roleRequest)
        {
            var existingRole = await _context.Roles.Include(r=> r.RolePermissions).FirstOrDefaultAsync(r=> r.Id == roleId);
            if (existingRole == null)
            {
                throw new Exception("Role not found");
            }
            existingRole.Name = roleRequest.Name;
            existingRole.Description = roleRequest.Description;
            var currentFeatureIds = existingRole.RolePermissions.Select(rp => rp.FeatureId).ToList();
            var newFeatureIds = roleRequest.FeatureIds.Distinct().ToList();
            var toRemove = existingRole.RolePermissions.Where(rp => !newFeatureIds.Contains(rp.FeatureId)).ToList();
            _context.RemoveRange(toRemove);
            var toAdd = newFeatureIds.Where(id => !currentFeatureIds.Contains(id)).Select(id=> new RolePermission { RoleId = roleId,FeatureId = id, IsEnabled = true}).ToList();
            _context.AddRange(toAdd);
            await _context.SaveChangesAsync();
            return existingRole;

        }

        public async Task<bool> DeleteRoleAsync(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null) { 
                return false;
            }

            _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            return true;
        }

        public async Task changeRoleAsync(long id, bool status)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                role.IsActive = status;
                await _context.SaveChangesAsync();
            }
        }




    }
}
