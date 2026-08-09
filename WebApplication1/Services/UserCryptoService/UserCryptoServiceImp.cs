using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.PaymentMethod;
using WebApplication1.Models;
using WebApplication1.Repositories.UserCryptoRepo;
using WebApplication1.Util;

namespace WebApplication1.Services.UserCryptoService;

public class UserCryptoServiceImp : IUserCryptoService
{
    private readonly IUserCryptoRepo _cryptoRepo;

    public UserCryptoServiceImp(IUserCryptoRepo cryptoRepo)
    {
        _cryptoRepo = cryptoRepo;
    }

    // Create the user's crypto payment method, or update it if one already exists (upsert).
    public async Task<ResponseDto<ResponseCryptoPaymentMethod>> AddCryptoUser(RequestCryptoPaymentMethod requestCrypto, Guid userId)
    {
        try
        {
            if (requestCrypto == null)
            {
                return new ResponseDto<ResponseCryptoPaymentMethod>()
                {
                    responseCode = Main.code400,
                    responseMessage = "Bad request.",
                    data = null
                };
            }

            if (userId == Guid.Empty)
            {
                return new ResponseDto<ResponseCryptoPaymentMethod>()
                {
                    responseCode = Main.code400,
                    responseMessage = "Your session has expired. Please sign in again.",
                    data = null
                };
            }

            var existing = await _cryptoRepo.GetByUserId(userId);
            UserCrypto userCrypto;
            if (existing == null)
            {
                // ---- create ----
                userCrypto = new UserCrypto
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    UsdcWalletNum = requestCrypto.UsdcWallet,
                    EthWalletNum = requestCrypto.EthWallet,
                    BtcWalletNum = requestCrypto.BtcWallet,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId.ToString(),
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = userId.ToString()
                };
                await _cryptoRepo.AddUserCrpyto(userCrypto);
            }
            else
            {
                // ---- edit (user already has a record) ----
                existing.UsdcWalletNum = requestCrypto.UsdcWallet;
                existing.EthWalletNum = requestCrypto.EthWallet;
                existing.BtcWalletNum = requestCrypto.BtcWallet;
                existing.IsActive = true;
                existing.UpdatedAt = DateTime.UtcNow;
                existing.UpdatedBy = userId.ToString();
                await _cryptoRepo.UpdateUserCrpyto(existing);
                userCrypto = existing;
            }

            return new ResponseDto<ResponseCryptoPaymentMethod>
            {
                responseCode = Main.code200,
                responseMessage = existing == null
                    ? "Crypto payment method saved."
                    : "Crypto payment method updated.",
                data = ToResponse(userCrypto)
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while saving your crypto details: {ex.Message}");
        }
    }

    public async Task<ResponseDto<ResponseCryptoPaymentMethod>> GetCryptoUser(Guid userId)
    {
        try
        {
            var crypto = await _cryptoRepo.GetByUserId(userId);
            return new ResponseDto<ResponseCryptoPaymentMethod>
            {
                responseCode = Main.code200,
                responseMessage = crypto == null ? "No crypto payment method yet." : "Success",
                data = crypto == null ? null : ToResponse(crypto)
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while loading your crypto details: {ex.Message}");
        }
    }

    private static ResponseCryptoPaymentMethod ToResponse(UserCrypto crypto) => new()
    {
        Id = crypto.Id,
        UsdcWalletNum = crypto.UsdcWalletNum,
        EthWalletNum = crypto.EthWalletNum,
        BtcWalletNum = crypto.BtcWalletNum,
        IsActive = crypto.IsActive
    };

    private static ResponseDto<ResponseCryptoPaymentMethod> Fail(int code, string message) => new()
    {
        responseCode = code,
        responseMessage = message,
        data = null
    };
}