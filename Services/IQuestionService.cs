using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IQuestionService
    {
        Task<PagedResponse<QuestionResponse>> GetPagedQuestionsAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<QuestionResponse>> GetQuestionsBySectionIdAsync(long sectionId);
        Task<QuestionResponse> GetQuestionByIdAsync(long questionId);
        Task<QuestionResponse> CreateQuestionAsync(QuestionRequest request);
        Task<QuestionResponse> UpdateQuestionAsync(long questionId, QuestionRequest request);
        Task<bool> DeleteQuestionAsync(long questionId);
        Task<bool> ChangeStatusAsync(long questionId, bool status);
    }
}
