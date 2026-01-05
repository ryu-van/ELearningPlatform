using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction tx);
        Task<IEnumerable<Transaction>> GetByOrderAsync(long orderId);
    }
}
