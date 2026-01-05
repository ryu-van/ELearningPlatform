using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
namespace E_learning_platform.Services
{
    public interface ILevelService
    {
        Task<PagedResponse<LevelResponse>> GetPagedAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<LevelResponse> GetByIdAsync(long id);
        Task<LevelResponse> CreateAsync(LevelRequest request);
        Task<LevelResponse> UpdateAsync(long id, LevelRequest request);
        Task<bool> DeleteAsync(long id);
        Task<bool> ChangeStatusAsync(long id, bool status);
    }
}
