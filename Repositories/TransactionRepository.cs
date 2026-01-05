using E_learning_platform.Data;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction tx)
        {
            _context.Transactions.Add(tx);
            await _context.SaveChangesAsync();
            return tx;
        }

        public async Task<IEnumerable<Transaction>> GetByOrderAsync(long orderId)
        {
            return await _context.Transactions
                .Where(t => t.OrderId == orderId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
