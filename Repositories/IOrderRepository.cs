using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<Order?> GetOrderByIdAsync(long id);
        Task<IEnumerable<Order>> GetByUserAsync(long userId);
        Task<bool> MarkPaidAsync(long orderId, string status);
        Task<bool> ConfirmOrderAsync(long orderId, long staffId);
    }
}
