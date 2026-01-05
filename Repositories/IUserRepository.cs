namespace E_learning_platform.Repositories
{
    public interface IUserRepository
    {
        Task<E_learning_platform.DTOs.Responses.PagedResponse<E_learning_platform.Models.User>> GetPagedUsersAsync(string? keyword, bool? isActive, int page, int pageSize);
        Task<E_learning_platform.Models.User?> GetByIdAsync(long id);
        Task<bool> ChangeStatusAsync(long id, bool status);
        Task<bool> DeleteAsync(long id);
    }
}
