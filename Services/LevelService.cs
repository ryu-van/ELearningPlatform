using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;
namespace E_learning_platform.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _repo;
        private readonly IMapper _mapper;
        public LevelService(ILevelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<bool> ChangeStatusAsync(long id, bool status)
        {
            return await _repo.ChangeStatusAsync(id, status);
        }
        public async Task<LevelResponse> CreateAsync(LevelRequest request)
        {
            var created = await _repo.CreateAsync(request);
            return _mapper.Map<LevelResponse>(created);
        }
        public async Task<bool> DeleteAsync(long id)
        {
            return await _repo.DeleteAsync(id);
        }
        public async Task<LevelResponse> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<LevelResponse>(entity);
        }
        public async Task<PagedResponse<LevelResponse>> GetPagedAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var paged = await _repo.GetPagedAsync(keyword, isActive, page, pageSize);
            var mapped = _mapper.Map<IEnumerable<LevelResponse>>(paged.Data);
            return new PagedResponse<LevelResponse>(mapped, paged.TotalItems, paged.CurrentPage, paged.PageSize);
        }
        public async Task<LevelResponse> UpdateAsync(long id, LevelRequest request)
        {
            var updated = await _repo.UpdateAsync(id, request);
            return _mapper.Map<LevelResponse>(updated);
        }
    }
}
