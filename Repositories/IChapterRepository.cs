using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IChapterRepository
    {
        Task<PagedResponse<Chapter>> GetPagedChaptersAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<Chapter>> GetChaptersByCourseIdAsync(long courseId);
        Task<Chapter> GetChapterByIdAsync(long chapterId);
        Task<Chapter> CreateChapterAsync(ChapterRequest request);
        Task<Chapter> UpdateChapterAsync(long chapterId, ChapterRequest request);
        Task<bool> DeleteChapterAsync(long chapterId);
        Task<bool> ChangeStatusAsync(long chapterId, bool status);
    }
}
