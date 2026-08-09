using WebApplication1.Models;

namespace WebApplication1.Repositories.TransactionRepo;

public interface ITransactionRepo
{
    Task<Transaction> AddTransaction(Transaction transaction);
}
