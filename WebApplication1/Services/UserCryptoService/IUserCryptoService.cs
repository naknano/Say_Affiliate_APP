using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.PaymentMethod;

namespace WebApplication1.Services.UserCryptoService;

public interface IUserCryptoService
{
    Task<ResponseDto<ResponseCryptoPaymentMethod>> AddCryptoUser(RequestCryptoPaymentMethod requestCrypto, Guid userId);

    Task<ResponseDto<ResponseCryptoPaymentMethod>> GetCryptoUser(Guid userId);
}