using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IQuestionRepository
    {
        Task<PagedResponse<Question>> GetPagedQuestionsAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<Question>> GetQuestionsBySectionIdAsync(long sectionId);
        Task<Question> GetQuestionByIdAsync(long questionId);
        Task<bool> IsOptionCorrectAsync(long questionId, long optionId);
        Task<Question> CreateQuestionAsync(QuestionRequest request);
        Task<Question> UpdateQuestionAsync(long questionId, QuestionRequest request);
        Task<bool> DeleteQuestionAsync(long questionId);
        Task<bool> ChangeStatusAsync(long questionId, bool status);
    }
}
