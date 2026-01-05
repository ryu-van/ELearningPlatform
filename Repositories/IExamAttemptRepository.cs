using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IExamAttemptRepository
    {
        Task<ExamAttempt> StartAttemptAsync(long userId, long examId, int attemptNumber, decimal maxScore);
        Task<ExamAttempt?> GetAttemptByIdAsync(long attemptId);
        Task<IEnumerable<ExamAttempt>> GetAttemptsByUserAsync(long userId, long? examId);
        Task<bool> FinishAttemptAsync(long attemptId, decimal totalScore, int timeSpentSeconds, bool isPassed);
        Task<UserAnswer> SubmitAnswerAsync(UserAnswer answer);
        Task<IEnumerable<UserAnswer>> GetAnswersByAttemptAsync(long attemptId);
        Task<decimal> GetMaxScoreForExamAsync(long examId);
        
        Task<bool> GradeAnswerAsync(long attemptId, long questionId, decimal score, bool isCorrect, long graderId);
        Task<bool> UpdateAttemptTotalScoreAsync(long attemptId);
    }
}
