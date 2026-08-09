using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.PaymentMethod;
using WebApplication1.Models;
using WebApplication1.Repositories.BankUserRepo;
using WebApplication1.Util;

namespace WebApplication1.Services.UserBankAccPaymentMethod;

public class BankUserServiceImp : IBankUserService
{
    private readonly IBankUserRepo _bankRepo;

    public BankUserServiceImp(IBankUserRepo bankRepo)
    {
        _bankRepo = bankRepo;
    }

    // Create the user's bank payment method, or update it if one already exists (upsert).
    public async Task<ResponseDto<ResponseBankPaymentMethod>> AddBankUser(RequestBankPaymentMethod request, Guid userId)
    {
        try
        {
            if (request == null)
            {
                return Fail(Main.code400, "Bad request.");
            }

            if (userId == Guid.Empty)
            {
                return Fail(Main.code400, "Your session has expired. Please sign in again.");
            }

            if (string.IsNullOrWhiteSpace(request.BankAccountNum))
            {
                return Fail(Main.code400, "Bank account number is required.");
            }

            Guid? bankId = Guid.TryParse(request.BankId, out var parsedBankId) ? parsedBankId : null;
            var now = DateTime.UtcNow;
            var existing = await _bankRepo.GetByUserId(userId);

            UserBank userBank;
            if (existing == null)
            {
                // ---- create (id is DB-generated identity) ----
                userBank = new UserBank
                {
                    UserId = userId,
                    BankId = bankId,
                    BankName = request.BankName,
                    AccountName = request.AccountName,
                    BankAccountNum = request.BankAccountNum,
                    IsActive = true,
                    CreatedBy = userId.ToString(),
                    CreatedAt = now,
                    UpdatedBy = userId.ToString(),
                    UpdatedAt = now
                };
                await _bankRepo.AddUserBank(userBank);
            }
            else
            {
                // ---- edit (user already has a record) ----
                existing.BankId = bankId;
                existing.BankName = request.BankName;
                existing.AccountName = request.AccountName;
                existing.BankAccountNum = request.BankAccountNum;
                existing.IsActive = true;
                existing.UpdatedBy = userId.ToString();
                existing.UpdatedAt = now;
                await _bankRepo.UpdateUserBank(existing);
                userBank = existing;
            }

            return new ResponseDto<ResponseBankPaymentMethod>
            {
                responseCode = Main.code200,
                responseMessage = existing == null
                    ? "Bank account saved."
                    : "Bank account updated.",
                data = ToResponse(userBank)
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while saving your bank details: {ex.Message}");
        }
    }

    public async Task<ResponseDto<ResponseBankPaymentMethod>> GetBankUser(Guid userId)
    {
        try
        {
            var bank = await _bankRepo.GetByUserId(userId);
            return new ResponseDto<ResponseBankPaymentMethod>
            {
                responseCode = Main.code200,
                responseMessage = bank == null ? "No bank account yet." : "Success",
                data = bank == null ? null : ToResponse(bank)
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while loading your bank details: {ex.Message}");
        }
    }

    private static ResponseBankPaymentMethod ToResponse(UserBank bank) => new()
    {
        Id = bank.Id,
        BankId = bank.BankId ?? Guid.Empty,
        BankName = bank.BankName,
        AccountName = bank.AccountName,
        BankAccountNum = bank.BankAccountNum,
        IsActive = bank.IsActive
    };

    private static ResponseDto<ResponseBankPaymentMethod> Fail(int code, string message) => new()
    {
        responseCode = code,
        responseMessage = message,
        data = null
    };
}
