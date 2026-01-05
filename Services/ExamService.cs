using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _repo;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> ChangeStatusAsync(long examId, bool status)
        {
            return _repo.ChangeStatusAsync(examId, status);
        }

        public async Task<ExamResponse> CreateExamAsync(ExamRequest request)
        {
            var created = await _repo.CreateExamAsync(request);
            return _mapper.Map<ExamResponse>(created);
        }

        public Task<bool> DeleteExamAsync(long examId)
        {
            return _repo.DeleteExamAsync(examId);
        }

        public async Task<ExamResponse> GetExamByIdAsync(long examId)
        {
            var entity = await _repo.GetExamByIdAsync(examId);
            return _mapper.Map<ExamResponse>(entity);
        }

        public async Task<PagedResponse<ExamResponse>> GetPagedExamsAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var paged = await _repo.GetPagedExamsAsync(keyword, isActive, page, pageSize);
            var mapped = _mapper.Map<IEnumerable<ExamResponse>>(paged.Data);
            return new PagedResponse<ExamResponse>(mapped, paged.TotalItems, paged.CurrentPage, paged.PageSize);
        }

        public async Task<ExamResponse> UpdateExamAsync(long examId, ExamRequest request)
        {
            var updated = await _repo.UpdateExamAsync(examId, request);
            return _mapper.Map<ExamResponse>(updated);
        }
    }
}
