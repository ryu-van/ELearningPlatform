using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface ICourseRepository
    {
        Task<PagedResponse<Course>> GetPagedCoursesAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<Course> GetCourseByIdAsync(long courseId);
        Task<Course> CreateCourseAsync(CourseRequest request);
        Task<Course> UpdateCourseAsync(long courseId, CourseRequest request);
        Task<bool> DeleteCourseAsync(long courseId);
        Task<bool> ChangeStatusAsync(long courseId, bool status);
    }
}
