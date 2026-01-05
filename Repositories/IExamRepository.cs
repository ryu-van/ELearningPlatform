using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IExamRepository
    {
        Task<PagedResponse<Exam>> GetPagedExamsAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<Exam> GetExamByIdAsync(long examId);
        Task<Exam> CreateExamAsync(ExamRequest request);
        Task<Exam> UpdateExamAsync(long examId, ExamRequest request);
        Task<bool> DeleteExamAsync(long examId);
        Task<bool> ChangeStatusAsync(long examId, bool status);
    }
}
