using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;

namespace WebApplication1.Repositories.TransactionRepo;

public class TransactionUnauthRepoImp : ITransactionUnauthRepo
{
    private readonly AppDBContext _context;

    public TransactionUnauthRepoImp(AppDBContext context)
    {
        _context = context;
    }

    public async Task<TransactionUnauth> AddTransactionUnauth(TransactionUnauth transaction)
    {
        await _context.TransactionsUnauth.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public Task<List<TransactionListItemDto>> GetUserTransactions(Guid userId)
    {
        var unauth = _context.TransactionsUnauth.AsNoTracking()
            .Where(t => t.UserId == userId)
            .Select(t => new TransactionListItemDto
            {
                Id = t.Id,
                CreatedAt = t.CreatedAt,
                BrokerName = null,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                Currency = t.Currency,
                Status = t.Status
            });

        var processed = _context.Transactions.AsNoTracking()
            .Where(t => t.UserId == userId)
            .Select(t => new TransactionListItemDto
            {
                Id = t.Id,
                CreatedAt = t.CreatedAt,
                BrokerName = null,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                Currency = t.Currency,
                Status = t.Status
            });

        // single UNION ALL query — one DB round trip, sorting done by the DB
        return unauth
            .Concat(processed)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}
