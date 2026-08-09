using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;

namespace WebApplication1.Services.Authentication;

public interface ITradingAccount
{
    Task<ResponseDto<UserTradingAccount>> AddTradingAccount(UserTradingAccountRequestDto request, string userId);

    Task<List<UserTradingAccountResponseDto>> GetTradingAccounts(string userId);

    Task<ResponseDto<UserTradingAccount>> UpdateTradingAccount(UserTradingAccountUpdateDto request, string userId);

    Task<ResponseDto<UserTradingAccount>> DeleteTradingAccount(string id, string userId);
}
