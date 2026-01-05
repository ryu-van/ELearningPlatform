using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IExamAttemptService
    {
        Task<ExamAttemptResponse> StartAttemptAsync(long userId, StartExamAttemptRequest request);
        Task<bool> SubmitAnswerAsync(long userId, SubmitAnswerRequest request);
        Task<ExamAttemptResponse> FinishAttemptAsync(long userId, long attemptId);
        Task<IEnumerable<ExamAttemptResponse>> GetMyAttemptsAsync(long userId, long? examId);
        Task<IEnumerable<UserAnswerResponse>> GetAttemptAnswersAsync(long userId, long attemptId);
        
        Task<bool> GradeEssayQuestionAsync(long graderId, GradeEssayRequest request);
    }
}
