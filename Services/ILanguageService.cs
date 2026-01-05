using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
namespace E_learning_platform.Services
{
    public interface ILanguageService
    {
        Task<PagedResponse<LanguageResponse>> GetPagedAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<LanguageResponse> GetByIdAsync(long id);
        Task<LanguageResponse> CreateAsync(LanguageRequest request);
        Task<LanguageResponse> UpdateAsync(long id, LanguageRequest request);
        Task<bool> DeleteAsync(long id);
        Task<bool> ChangeStatusAsync(long id, bool status);
    }
}
