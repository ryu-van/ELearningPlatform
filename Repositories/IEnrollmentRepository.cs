using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<PagedResponse<Enrollment>> GetPagedEnrollmentsAsync(string keyword, string status, int page, int pageSize);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(long userId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(long courseId);
        Task<Enrollment?> GetEnrollmentByIdAsync(long id);
        Task<Enrollment> CreateEnrollmentAsync(long userId, long courseId);
        Task<bool> DeleteEnrollmentAsync(long id);
        Task<bool> UpdateProgressAsync(long id, decimal progress);
        Task<bool> IsEnrolledAsync(long userId, long courseId);
    }
}
