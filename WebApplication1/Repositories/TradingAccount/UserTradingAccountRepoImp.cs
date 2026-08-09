using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;
using WebApplication1.Repositories.broker;

namespace WebApplication1.Repositories.TradingAccount;

public class UserTradingAccountRepoImp : IUserTradingAccountRepo
{
    private readonly AppDBContext _context;
    private readonly IBrokerRepository _brokerRepository;

    public UserTradingAccountRepoImp(AppDBContext dbContext, IBrokerRepository brokerRepository)
    {
        _context = dbContext;
        _brokerRepository = brokerRepository;
    }

    public async Task<UserTradingAccount> AddUserTradingAccount(UserTradingAccount userTradingAccount)
    {
        await _context.UserTradingAccounts.AddAsync(userTradingAccount);
        await _context.SaveChangesAsync();
        return userTradingAccount;
    }

    public Task<bool> ExistsAsync(string userId, string tradingAcc)
    {
        return _context.UserTradingAccounts
            .AsNoTracking()
            .AnyAsync(x => x.UserId == userId && x.TradingAcc == tradingAcc);
    }

    public async Task<List<UserTradingAccountResponseDto>> GetByUserIdAsync(string userId)
    {
        // user-specific rows come from the DB...
        var accounts = await _context.UserTradingAccounts.AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new UserTradingAccountResponseDto
            {
                Id = a.Id,
                TradingAcc = a.TradingAcc ?? string.Empty,
                BrokerId = a.BrokerId,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        if (accounts.Count == 0) return accounts;

        // ...but broker names resolve from the cached broker list (no join on tbl_broker)
        var brokerNames = (await _brokerRepository.GetBrokerAsync())
            .ToDictionary(b => b.Id, b => b.Name);

        foreach (var acc in accounts)
        {
            if (acc.BrokerId.HasValue && brokerNames.TryGetValue(acc.BrokerId.Value, out var name))
                acc.BrokerName = name;
        }

        return accounts;
    }

    public Task<UserTradingAccount?> GetEntityAsync(Guid id, string userId)
    {
        return _context.UserTradingAccounts
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }

    public async Task<UserTradingAccount> UpdateAsync(UserTradingAccount userTradingAccount)
    {
        await _context.SaveChangesAsync();
        return userTradingAccount;
    }

    public async Task DeleteAsync(UserTradingAccount userTradingAccount)
    {
        _context.UserTradingAccounts.Remove(userTradingAccount);
        await _context.SaveChangesAsync();
    }
}