using E_learning_platform.Data;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public async Task<bool> changeStatusFeature(long id, bool status)
        {
            var feature = await applicationDbContext.Features.FindAsync(id);
            if (feature == null)
                return false;

            feature.IsActive = status;
            await applicationDbContext.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Feature>> GetAllAsync()
        {
           return await applicationDbContext.Features.Where(f => f.IsActive).OrderBy(f=>f.Name).ToListAsync();
        }
    } 
}
