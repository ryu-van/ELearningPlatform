using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface ILessonService
    {
        Task<PagedResponse<LessonResponse>> GetPagedLessonsAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<LessonResponse>> GetLessonsByChapterIdAsync(long chapterId);
        Task<LessonResponse> GetLessonByIdAsync(long lessonId);
        Task<LessonResponse> CreateLessonAsync(LessonRequest request);
        Task<LessonResponse> UpdateLessonAsync(long lessonId, LessonRequest request);
        Task<bool> DeleteLessonAsync(long lessonId);
        Task<bool> ChangeStatusAsync(long lessonId, bool status);
    }
}
