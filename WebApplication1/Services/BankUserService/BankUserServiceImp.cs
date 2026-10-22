using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.PaymentMethod;
using WebApplication1.Models;
using WebApplication1.Repositories.BankUserRepo;
using WebApplication1.Util;

namespace WebApplication1.Services.UserBankAccPaymentMethod;

public class BankUserServiceImp : IBankUserService
{
    // Fixed values for a manual bank (account-number) payment method.
    private const string BankPaymentMethod = "Account number"; // tbl_banks.payment_method (text)
    private const int UserBankPaymentMethod = 1;               // tbl_banks_user.payment_method (int)
    private const decimal AverageTransaction = 1_000_000m;     // tbl_banks.average_transaction
    private const decimal MinTransaction = 0m;                 // tbl_banks.min_transaction

    private readonly IBankUserRepo _bankRepo;

    public BankUserServiceImp(IBankUserRepo bankRepo)
    {
        _bankRepo = bankRepo;
    }

    // Add the user's bank: create a bank record (tbl_banks) and map it to the
    // user via tbl_banks_user (bank_id). One bank + one bank-user per person,
    // so a repeat call updates the existing pair instead of duplicating.
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

            var now = DateTime.UtcNow;
            var uid = userId.ToString();
            var accountNum = request.BankAccountNum.Trim();
            var existing = await _bankRepo.GetByUserId(userId);

            // ---- resolve (and reuse) the linked bank, else start a new one ----
            var bank = existing?.BankId != null ? await _bankRepo.GetBankById(existing.BankId.Value) : null;
            var bankIsNew = bank == null;
            bank ??= new Bank { Id = Guid.NewGuid(), CreatedBy = userId, CreatedAt = now };

            bank.BankName = request.BankName;
            bank.AccountNumber = accountNum;
            bank.AccountName = request.AccountName;
            bank.PaymentMethod = BankPaymentMethod;
            bank.AverageTransaction = AverageTransaction;
            bank.MinTransaction = MinTransaction;
            bank.Currency = "USD";
            bank.Status = "active";
            if (!bankIsNew)
            {
                bank.UpdatedBy = userId;
                bank.UpdatedAt = now;
            }

            // ---- resolve (and reuse) the user->bank mapping, else start a new one ----
            var userBankIsNew = existing == null;
            var userBank = existing ?? new UserBank { UserId = userId, CreatedBy = uid, CreatedAt = now };

            userBank.BankId = bank.Id;
            userBank.BankName = request.BankName;
            userBank.AccountName = request.AccountName;
            userBank.BankAccountNum = accountNum;
            userBank.PaymentMethod = UserBankPaymentMethod;
            userBank.IsActive = true;
            userBank.UpdatedBy = uid;
            userBank.UpdatedAt = now;

            // ---- persist both rows atomically ----
            if (!await _bankRepo.SaveBankAndMappingAsync(bank, bankIsNew, userBank, userBankIsNew))
            {
                return Fail(Main.code500, "An error occurred while saving your bank details.");
            }

            return new ResponseDto<ResponseBankPaymentMethod>
            {
                responseCode = Main.code200,
                responseMessage = userBankIsNew ? "Bank account saved." : "Bank account updated.",
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
