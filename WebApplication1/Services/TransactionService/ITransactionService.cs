using WebApplication1.Data.DTO.TradingAccount;

namespace WebApplication1.Services.TransactionService;

public interface ITransactionService
{
    // combined transaction history for a user (pending + processed)
    Task<List<TransactionListItemDto>> GetUserTransactions(Guid userId);
}
