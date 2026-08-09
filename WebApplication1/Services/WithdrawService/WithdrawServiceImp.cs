using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Withdraw;
using WebApplication1.Models;
using WebApplication1.Repositories.BankUserRepo;
using WebApplication1.Repositories.TransactionRepo;
using WebApplication1.Repositories.UserCryptoRepo;
using WebApplication1.Services.Authentication;
using WebApplication1.Util;

namespace WebApplication1.Services.WithdrawService;

public class WithdrawServiceImp : IWithdrawService
{
    private readonly ITransactionUnauthRepo _transactionUnauthRepo;
    private readonly IBankUserRepo _bankUserRepo;
    private readonly IUserCryptoRepo _userCryptoRepo;
    private readonly IUserDetailService _userDetailService;
    private readonly AppDBContext _context;

    public WithdrawServiceImp(ITransactionUnauthRepo transactionUnauthRepo,
        IBankUserRepo bankUserRepo,
        IUserCryptoRepo userCryptoRepo,
        IUserDetailService userDetailService,
        AppDBContext context)
    {
        _transactionUnauthRepo = transactionUnauthRepo;
        _bankUserRepo = bankUserRepo;
        _userCryptoRepo = userCryptoRepo;
        _userDetailService = userDetailService;
        _context = context;
    }

    public async Task<ResponseDto<string>> PostWithdraw(RequestWithdrawDto request, string userId, string email)
    {
        try
        {
            if (request is null)
                return Fail(Main.code400, "Bad request.");

            if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var uid))
                return Fail(Main.code400, "Your session has expired. Please sign in again.");

            if (request.Amount <= 0)
                return Fail(Main.code400, "Please enter a valid amount.");

            // resolve the payout destination (bank or crypto), server-side
            var isCrypto = string.Equals(request.Method?.Trim(), "crypto", StringComparison.OrdinalIgnoreCase);
            var destination = isCrypto
                ? await ResolveCryptoAsync(request, uid)
                : await ResolveBankAsync(request, uid);

            if (destination.Error is not null)
                return Fail(Main.code400, destination.Error);

            var user = (await _userDetailService.loadUserAsync(userId))?.data;

            var transaction = new TransactionUnauth
            {
                Id = await GenerateTransactionAsync(),
                UserId = uid,
                BankId = destination.BankId,
                TransactionType = "Withdrawal",
                Status = "pending",
                Amount = request.Amount,
                Currency = string.IsNullOrWhiteSpace(request.Currency) ? "USD" : request.Currency,
                MerchantDescription = destination.Description,
                CustomerEmail = user?.Email ?? email,
                CustomerCountryCode = user?.CountryCode,
                CustomerBankAccountName = destination.AccountName,
                CustomerBankAccountNum = destination.AccountNum,
                CreatedAt = DateTime.UtcNow
            };

            await _transactionUnauthRepo.AddTransactionUnauth(transaction);

            return new ResponseDto<string>
            {
                responseCode = Main.code200,
                responseMessage = "Withdrawal request submitted.",
                data = transaction.Id
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while submitting your withdrawal: {ex.Message}");
        }
    }

    private async Task<Destination> ResolveCryptoAsync(RequestWithdrawDto request, Guid uid)
    {
        var crypto = await _userCryptoRepo.GetByUserId(uid);
        if (crypto is null)
            return Destination.Invalid("You haven't added a crypto wallet yet.");

        var network = (request.CryptoNetwork ?? "").Trim().ToUpperInvariant();
        var wallet = network switch
        {
            "USDC" => crypto.UsdcWalletNum,
            "ETH" => crypto.EthWalletNum,
            "BTC" => crypto.BtcWalletNum,
            _ => null
        };

        if (string.IsNullOrWhiteSpace(wallet))
            return Destination.Invalid("Please select a wallet that has an address saved.");

        return new Destination(
            accountName: $"{network} Wallet",
            accountNum: wallet,
            description: $"Cashback withdrawal to {network} wallet {wallet}");
    }

    private async Task<Destination> ResolveBankAsync(RequestWithdrawDto request, Guid uid)
    {
        if (!int.TryParse(request.UserBankId, out var bankRecordId))
            return Destination.Invalid("Please select a bank.");

        var bank = await _bankUserRepo.GetByIdAndUser(bankRecordId, uid);
        if (bank is null)
            return Destination.Invalid("Bank account not found.");

        return new Destination(
            accountName: bank.AccountName,
            accountNum: bank.BankAccountNum,
            description: $"Cashback withdrawal to {bank.BankName} - {bank.BankAccountNum}",
            bankId: bank.BankId);
    }

    // resolved payout destination (or a validation error)
    private sealed class Destination
    {
        public string? AccountName { get; }
        public string? AccountNum { get; }
        public string? Description { get; }
        public Guid? BankId { get; }
        public string? Error { get; }

        public Destination(string accountName, string accountNum, string description, Guid? bankId = null)
        {
            AccountName = accountName;
            AccountNum = accountNum;
            Description = description;
            BankId = bankId;
        }

        private Destination(string error) => Error = error;

        public static Destination Invalid(string error) => new(error);
    }

    private async Task<string> GenerateTransactionAsync()
    {
        var sequence = await _context.Database
            .SqlQuery<long>($"""
                SELECT nextval('public.transaction_id_seq') AS "Value"
            """)
            .FirstAsync();

        return $"TXN-{sequence:0000000000}";
    }

    private static ResponseDto<string> Fail(int code, string message) => new()
    {
        responseCode = code,
        responseMessage = message,
        data = null
    };
}
