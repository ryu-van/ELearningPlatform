using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
namespace E_learning_platform.Repositories
{
    public interface ILevelRepository
    {
        Task<PagedResponse<Level>> GetPagedAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<Level> GetByIdAsync(long id);
        Task<Level> CreateAsync(LevelRequest request);
        Task<Level> UpdateAsync(long id, LevelRequest request);
        Task<bool> DeleteAsync(long id);
        Task<bool> ChangeStatusAsync(long id, bool status);
    }
}
