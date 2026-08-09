using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.PaymentMethod;

namespace WebApplication1.Services.UserBankAccPaymentMethod;

public interface IBankUserService
{
    Task<ResponseDto<ResponseBankPaymentMethod>> AddBankUser(RequestBankPaymentMethod request, Guid userId);

    Task<ResponseDto<ResponseBankPaymentMethod>> GetBankUser(Guid userId);
}
