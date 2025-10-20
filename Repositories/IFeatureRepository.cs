using E_learning_platform.Models;
using System.Threading.Tasks;

namespace E_learning_platform.Repositories
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetAllAsync();
        
        Task<bool> changeStatusFeature(long id,bool status);
    }
}
