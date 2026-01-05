using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IReviewRepository
    {
        Task<PagedResponse<CourseReview>> GetReviewsByCourseIdAsync(long courseId, int page, int pageSize);
        Task<CourseReview> AddReviewAsync(CourseReview review);
        Task<bool> HasUserReviewedCourseAsync(long userId, long courseId);
        Task<decimal> GetAverageRatingAsync(long courseId);
        Task<bool> DeleteReviewAsync(long id);
        Task<CourseReview?> GetReviewByIdAsync(long id);
    }
}
