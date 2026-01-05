using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IEnrollmentService
    {
        Task<PagedResponse<EnrollmentResponse>> GetPagedEnrollmentsAsync(string keyword, string status, int page, int pageSize);
        Task<IEnumerable<EnrollmentResponse>> GetMyEnrollmentsAsync(long userId);
        Task<IEnumerable<EnrollmentResponse>> GetCourseEnrollmentsAsync(long courseId);
        Task<EnrollmentResponse> EnrollUserAsync(long userId, long courseId);
        Task<bool> UnenrollUserAsync(long enrollmentId);
        Task<bool> UpdateProgressAsync(long enrollmentId, decimal progress);
        Task<bool> IsEnrolledAsync(long userId, long courseId);
    }
}
