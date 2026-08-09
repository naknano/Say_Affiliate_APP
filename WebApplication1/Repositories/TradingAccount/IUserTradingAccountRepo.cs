using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;

namespace WebApplication1.Repositories.TradingAccount;

public interface IUserTradingAccountRepo
{
    Task<UserTradingAccount> AddUserTradingAccount(UserTradingAccount userTradingAccount);

    Task<bool> ExistsAsync(string userId, string tradingAcc);

    Task<List<UserTradingAccountResponseDto>> GetByUserIdAsync(string userId);

    Task<UserTradingAccount?> GetEntityAsync(Guid id, string userId);

    Task<UserTradingAccount> UpdateAsync(UserTradingAccount userTradingAccount);

    Task DeleteAsync(UserTradingAccount userTradingAccount);
}