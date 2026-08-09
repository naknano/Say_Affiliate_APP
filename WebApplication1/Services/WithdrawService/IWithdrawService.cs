using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Withdraw;

namespace WebApplication1.Services.WithdrawService;

public interface IWithdrawService
{
    Task<ResponseDto<string>> PostWithdraw(RequestWithdrawDto request, string userId, string email);
}
