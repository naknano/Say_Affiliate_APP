using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;

namespace WebApplication1.Repositories.TransactionRepo;

public interface ITransactionUnauthRepo
{
    Task<TransactionUnauth> AddTransactionUnauth(TransactionUnauth transaction);

    // combined history: pending from tbl_transactions_unauth + approved/rejected from tbl_transactions
    Task<List<TransactionListItemDto>> GetUserTransactions(Guid userId);
}
