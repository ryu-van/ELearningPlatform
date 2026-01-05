using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(long userId, CreateOrderRequest request);
        Task<bool> MarkPaidAsync(long orderId, string status, string gateway, decimal amount, string? responseCode, string? responseMessage);
        Task<bool> ConfirmOrderAsync(long orderId, long staffId);
        Task<IEnumerable<OrderResponse>> GetMyOrdersAsync(long userId);
        Task<OrderResponse?> GetOrderByIdAsync(long id);
    }
}
