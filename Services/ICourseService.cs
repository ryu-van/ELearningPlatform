using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface ICourseService
    {
        Task<PagedResponse<CourseResponse>> GetPagedCoursesAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<CourseResponse> GetCourseByIdAsync(long courseId);
        Task<CourseResponse> CreateCourseAsync(CourseRequest request);
        Task<CourseResponse> UpdateCourseAsync(long courseId, CourseRequest request);
        Task<bool> DeleteCourseAsync(long courseId);
        Task<bool> ChangeStatusAsync(long courseId, bool status);
    }
}
