using AutoMapper;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResponse<UserResponse>> GetPagedUsersAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var pagedUsers = await _repo.GetPagedUsersAsync(keyword, isActive, page, pageSize);
            var mapped = _mapper.Map<IEnumerable<UserResponse>>(pagedUsers.Data);
            return new PagedResponse<UserResponse>(mapped, pagedUsers.TotalItems, pagedUsers.CurrentPage, pagedUsers.PageSize);
        }

        public async Task<UserResponse?> GetByIdAsync(long id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserResponse>(user);
        }

        public Task<bool> ChangeStatusAsync(long id, bool status)
        {
            return _repo.ChangeStatusAsync(id, status);
        }

        public Task<bool> DeleteAsync(long id)
        {
            return _repo.DeleteAsync(id);
        }
    }
}
