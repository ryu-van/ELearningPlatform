using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IReviewService
    {
        Task<PagedResponse<ReviewResponse>> GetReviewsByCourseIdAsync(long courseId, int page, int pageSize);
        Task<ReviewResponse> AddReviewAsync(long userId, ReviewRequest request);
        Task<bool> DeleteReviewAsync(long id, long userId, bool isAdmin);
    }
}
