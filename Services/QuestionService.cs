using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> ChangeStatusAsync(long questionId, bool status)
        {
            return _repo.ChangeStatusAsync(questionId, status);
        }

        public async Task<QuestionResponse> CreateQuestionAsync(QuestionRequest request)
        {
            var created = await _repo.CreateQuestionAsync(request);
            return _mapper.Map<QuestionResponse>(created);
        }

        public Task<bool> DeleteQuestionAsync(long questionId)
        {
            return _repo.DeleteQuestionAsync(questionId);
        }

        public async Task<QuestionResponse> GetQuestionByIdAsync(long questionId)
        {
            var entity = await _repo.GetQuestionByIdAsync(questionId);
            return _mapper.Map<QuestionResponse>(entity);
        }

        public async Task<IEnumerable<QuestionResponse>> GetQuestionsBySectionIdAsync(long sectionId)
        {
            var items = await _repo.GetQuestionsBySectionIdAsync(sectionId);
            return _mapper.Map<IEnumerable<QuestionResponse>>(items);
        }

        public async Task<PagedResponse<QuestionResponse>> GetPagedQuestionsAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var paged = await _repo.GetPagedQuestionsAsync(keyword, isActive, page, pageSize);
            var mapped = _mapper.Map<IEnumerable<QuestionResponse>>(paged.Data);
            return new PagedResponse<QuestionResponse>(mapped, paged.TotalItems, paged.CurrentPage, paged.PageSize);
        }

        public async Task<QuestionResponse> UpdateQuestionAsync(long questionId, QuestionRequest request)
        {
            var updated = await _repo.UpdateQuestionAsync(questionId, request);
            return _mapper.Map<QuestionResponse>(updated);
        }
    }
}
