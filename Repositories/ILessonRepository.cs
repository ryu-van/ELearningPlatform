using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface ILessonRepository
    {
        Task<PagedResponse<Lesson>> GetPagedLessonsAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<Lesson>> GetLessonsByChapterIdAsync(long chapterId);
        Task<Lesson> GetLessonByIdAsync(long lessonId);
        Task<Lesson> CreateLessonAsync(LessonRequest request);
        Task<Lesson> UpdateLessonAsync(long lessonId, LessonRequest request);
        Task<bool> DeleteLessonAsync(long lessonId);
        Task<bool> ChangeStatusAsync(long lessonId, bool status);
    }
}
