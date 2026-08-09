using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Repositories.TransactionRepo;

namespace WebApplication1.Services.TransactionService;

public class TransactionServiceImp : ITransactionService
{
    private readonly ITransactionUnauthRepo _transactionUnauthRepo;

    public TransactionServiceImp(ITransactionUnauthRepo transactionUnauthRepo)
    {
        _transactionUnauthRepo = transactionUnauthRepo;
    }

    public Task<List<TransactionListItemDto>> GetUserTransactions(Guid userId)
    {
        if (userId == Guid.Empty)
            return Task.FromResult(new List<TransactionListItemDto>());

        return _transactionUnauthRepo.GetUserTransactions(userId);
    }
}
