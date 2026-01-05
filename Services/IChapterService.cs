using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IChapterService
    {
        Task<PagedResponse<ChapterResponse>> GetPagedChaptersAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<ChapterResponse>> GetChaptersByCourseIdAsync(long courseId);
        Task<ChapterResponse> GetChapterByIdAsync(long chapterId);
        Task<ChapterResponse> CreateChapterAsync(ChapterRequest request);
        Task<ChapterResponse> UpdateChapterAsync(long chapterId, ChapterRequest request);
        Task<bool> DeleteChapterAsync(long chapterId);
        Task<bool> ChangeStatusAsync(long chapterId, bool status);
    }
}
