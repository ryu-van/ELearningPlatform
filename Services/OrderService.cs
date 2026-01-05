using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ITransactionRepository transactionRepository,
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _transactionRepository = transactionRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> CreateOrderAsync(long userId, CreateOrderRequest request)
        {
            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
            if (course == null || !course.IsActive)
                throw new Exception("Khóa học không tồn tại hoặc đã vô hiệu hóa");

            var order = new Order
            {
                UserId = userId,
                CourseId = request.CourseId,
                Code = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6]}",
                BasisAmount = course.Price,
                FinalAmount = course.Price, 
                Paymentstatus = "Pending",
                PaymentMethod = request.PaymentMethod,
                CreatedAt = DateTime.UtcNow
            };
            await _orderRepository.CreateAsync(order);
            var created = await _orderRepository.GetOrderByIdAsync(order.Id);
            var resp = _mapper.Map<OrderResponse>(created);
            resp.CourseTitle = course.Title ?? string.Empty;
            return resp;
        }

        public async Task<bool> MarkPaidAsync(long orderId, string status, string gateway, decimal amount, string? responseCode, string? responseMessage)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found");

            await _orderRepository.MarkPaidAsync(orderId, status);
            var tx = new Transaction
            {
                OrderId = orderId,
                Gateway = gateway,
                Amount = amount,
                Status = status,
                ResponseCode = responseCode,
                ResponseMessage = responseMessage,
                CreatedAt = DateTime.UtcNow
            };
            await _transactionRepository.CreateAsync(tx);

            if (status == "Paid")
            {
                var already = await _enrollmentRepository.IsEnrolledAsync(order.UserId, order.CourseId);
                if (!already)
                {
                    await _enrollmentRepository.CreateEnrollmentAsync(order.UserId, order.CourseId);
                }
            }
            return true;
        }

        public async Task<bool> ConfirmOrderAsync(long orderId, long staffId)
        {
            return await _orderRepository.ConfirmOrderAsync(orderId, staffId);
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(long id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return null;

            var response = _mapper.Map<OrderResponse>(order);
            // Manually map if needed or ensure AutoMapper does it
            response.CourseTitle = order.Course?.Title ?? string.Empty;
            // response.UserName = order.Register?.FullName ?? string.Empty; // if OrderResponse has UserName
            return response;
        }

        public async Task<IEnumerable<OrderResponse>> GetMyOrdersAsync(long userId)
        {
            var orders = await _orderRepository.GetByUserAsync(userId);
            var resps = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            foreach (var r in resps)
            {
                var o = orders.First(x => x.Id == r.Id);
                r.CourseTitle = o.Course?.Title ?? string.Empty;
            }
            return resps;
        }
    }
}
