namespace E_learning_platform.Services
{
    public interface IUserService
    {
        Task<E_learning_platform.DTOs.Responses.PagedResponse<E_learning_platform.DTOs.Responses.UserResponse>> GetPagedUsersAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<E_learning_platform.DTOs.Responses.UserResponse?> GetByIdAsync(long id);
        Task<bool> ChangeStatusAsync(long id, bool status);
        Task<bool> DeleteAsync(long id);
    }
}
