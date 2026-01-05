using E_learning_platform.Data;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetByUserAsync(long userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Course)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(long id)
        {
            return await _context.Orders
                .Include(o => o.Course)
                .Include(o => o.Register)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> MarkPaidAsync(long orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            order.Paymentstatus = status;
            order.PaidAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmOrderAsync(long orderId, long staffId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.IsConfirmed = true;
            order.ConfirmedAt = DateTime.UtcNow;
            order.ConfirmedBy = staffId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
