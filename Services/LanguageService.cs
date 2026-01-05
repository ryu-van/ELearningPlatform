using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;
namespace E_learning_platform.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _repo;
        private readonly IMapper _mapper;
        public LanguageService(ILanguageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<bool> ChangeStatusAsync(long id, bool status)
        {
            return await _repo.ChangeStatusAsync(id, status);
        }
        public async Task<LanguageResponse> CreateAsync(LanguageRequest request)
        {
            var created = await _repo.CreateAsync(request);
            return _mapper.Map<LanguageResponse>(created);
        }
        public async Task<bool> DeleteAsync(long id)
        {
            return await _repo.DeleteAsync(id);
        }
        public async Task<LanguageResponse> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<LanguageResponse>(entity);
        }
        public async Task<PagedResponse<LanguageResponse>> GetPagedAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var paged = await _repo.GetPagedAsync(keyword, isActive, page, pageSize);
            var mapped = _mapper.Map<IEnumerable<LanguageResponse>>(paged.Data);
            return new PagedResponse<LanguageResponse>(mapped, paged.TotalItems, paged.CurrentPage, paged.PageSize);
        }
        public async Task<LanguageResponse> UpdateAsync(long id, LanguageRequest request)
        {
            var updated = await _repo.UpdateAsync(id, request);
            return _mapper.Map<LanguageResponse>(updated);
        }
    }
}
