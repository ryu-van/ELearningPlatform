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

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await applicationDbContext.Roles.ToListAsync();
        } 
        public async Task<Role?> GetRoleByIdAsync(long id)
        {
            return await applicationDbContext.Roles.FindAsync(id);
        }

        public async Task CreateRoleAsync(RoleRequest roleRequest)
        {
            var newRole = new Role
            {
                Name = roleRequest.Name!
            };

            applicationDbContext.Roles.Add(newRole);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(long id, RoleRequest roleRequest)
        {
            var existingRole = await applicationDbContext.Roles.FindAsync(id);
            if (existingRole != null)
            {
                existingRole.Name = roleRequest.Name;
                await applicationDbContext.SaveChangesAsync();
            }
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



    }
}
