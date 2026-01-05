using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
namespace E_learning_platform.Repositories
{
    public interface ILanguageRepository
    {
        Task<PagedResponse<Language>> GetPagedAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<Language> GetByIdAsync(long id);
        Task<Language> CreateAsync(LanguageRequest request);
        Task<Language> UpdateAsync(long id, LanguageRequest request);
        Task<bool> DeleteAsync(long id);
        Task<bool> ChangeStatusAsync(long id, bool status);
    }
}
