using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IExamService
    {
        Task<PagedResponse<ExamResponse>> GetPagedExamsAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<ExamResponse> GetExamByIdAsync(long examId);
        Task<ExamResponse> CreateExamAsync(ExamRequest request);
        Task<ExamResponse> UpdateExamAsync(long examId, ExamRequest request);
        Task<bool> DeleteExamAsync(long examId);
        Task<bool> ChangeStatusAsync(long examId, bool status);
    }
}
