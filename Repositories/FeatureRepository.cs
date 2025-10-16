using E_learning_platform.Data;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
namespace E_learning_platform.Repositories
{
    public class FeatureRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public FeatureRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<Feature>> GetAllAsync()
        {
            return await applicationDbContext.Features
                .Where(f => f.IsActive)
                .OrderBy(f => f.Name)
                .ToListAsync();
        }
        public async Task disableFeature(long id)
        {
            var feature = await applicationDbContext.Features.FindAsync(id);
            if (feature != null)
            {
                feature.IsActive = false;
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
