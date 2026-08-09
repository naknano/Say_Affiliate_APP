using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories.TransactionRepo;

public class TransactionRepoImp : ITransactionRepo
{
    private readonly AppDBContext _context;

    public TransactionRepoImp(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Transaction> AddTransaction(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}
