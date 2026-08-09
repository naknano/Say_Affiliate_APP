using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Dashboard;
using WebApplication1.Data.DTO.PaymentMethod;
using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Repositories.BankUserRepo;
using WebApplication1.Repositories.broker;
using WebApplication1.Repositories.UserCryptoRepo;
using WebApplication1.Services.Authentication;
using WebApplication1.Services.TransactionService;
using WebApplication1.Services.UserBankAccPaymentMethod;
using WebApplication1.Services.UserCryptoService;

namespace WebApplication1.Controllers.Profile;

[Authorize]
public class ProfileController : Controller
{
    private readonly ITradingAccount _tradingAccountService;
    private readonly IBrokerRepository _brokerRepository;
    private readonly IUserCryptoService _userCryptoService;
    private readonly IBankUserService _bankUserService;
    private readonly IUserDetailService _userDetailService;
    private readonly IBankUserRepo _bankUserRepo;
    private readonly IUserCryptoRepo _userCryptoRepo;
    private readonly ITransactionService _transactionService;

    public ProfileController(ITradingAccount tradingAccountService
        , IBrokerRepository brokerRepository
        , IUserCryptoService userCryptoService
        , IBankUserService bankUserService
        , IUserDetailService userDetailService
        , IBankUserRepo bankUserRepo
        , IUserCryptoRepo userCryptoRepo
        , ITransactionService transactionService)
    {
        _tradingAccountService = tradingAccountService;
        _brokerRepository = brokerRepository;
        _userCryptoService = userCryptoService;
        _bankUserService = bankUserService;
        _userDetailService = userDetailService;
        _bankUserRepo = bankUserRepo;
        _userCryptoRepo = userCryptoRepo;
        _transactionService = transactionService;
    }

    public IActionResult Index() => RedirectToAction(nameof(Dashboard));

    // ---- Dashboard sections (one page each) ----
    public async Task<IActionResult> Dashboard()
    {
        var userId = CurrentUserId();
        var uid = CurrentUserGuid();

        var accounts = await _tradingAccountService.GetTradingAccounts(userId) ?? new();
        var txns = await _transactionService.GetUserTransactions(uid) ?? new();
        var user = (await _userDetailService.loadUserAsync(userId))?.data;

        static bool IsWithdrawal(string? t) =>
            (t ?? "").Trim().ToLowerInvariant() is "withdrawal" or "withdraw";
        static bool IsSettled(string? s) =>
            (s ?? "").Trim().ToLowerInvariant() is "approved" or "completed" or "success" or "paid" or "done" or "settled";
        static bool IsPending(string? s) =>
            string.Equals((s ?? "").Trim(), "pending", StringComparison.OrdinalIgnoreCase);

        var credits = txns.Where(t => !IsWithdrawal(t.TransactionType)).ToList();
        var debits  = txns.Where(t =>  IsWithdrawal(t.TransactionType)).ToList();

        var totalEarned     = credits.Where(t => IsSettled(t.Status)).Sum(t => t.Amount);
        var totalWithdrawn  = debits.Where(t => IsSettled(t.Status)).Sum(t => t.Amount);
        var pendingWithdrawn = debits.Where(t => IsPending(t.Status)).Sum(t => t.Amount);
        var balance = totalEarned - totalWithdrawn - pendingWithdrawn;
        if (balance < 0) balance = 0;

        // ---- monthly series (last 8 months) ----
        var now = DateTime.UtcNow;
        var months = new List<string>();
        var earnedSeries = new List<decimal>();
        var withdrawnSeries = new List<decimal>();
        for (var i = 7; i >= 0; i--)
        {
            var start = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(-i);
            var end = start.AddMonths(1);
            months.Add(start.ToString("MMM"));
            earnedSeries.Add(credits.Where(t => IsSettled(t.Status) && t.CreatedAt >= start && t.CreatedAt < end).Sum(t => t.Amount));
            withdrawnSeries.Add(debits.Where(t => IsSettled(t.Status) && t.CreatedAt >= start && t.CreatedAt < end).Sum(t => t.Amount));
        }

        // ---- year-over-year on earnings ----
        var thisYear = credits.Where(t => IsSettled(t.Status) && t.CreatedAt.Year == now.Year).Sum(t => t.Amount);
        var lastYear = credits.Where(t => IsSettled(t.Status) && t.CreatedAt.Year == now.Year - 1).Sum(t => t.Amount);
        var yoy = lastYear > 0 ? (double)((thisYear - lastYear) / lastYear) * 100.0 : (thisYear > 0 ? 100.0 : 0.0);

        // ---- recent activity ----
        var recent = txns
            .OrderByDescending(t => t.CreatedAt)
            .Take(6)
            .Select(t =>
            {
                var isWd = IsWithdrawal(t.TransactionType);
                var status = IsSettled(t.Status) ? "settled" : IsPending(t.Status) ? "pending" : "rejected";
                return new DashboardActivityDto
                {
                    Title = isWd ? "Withdrawal" : "Cashback rebate",
                    Subtitle = string.IsNullOrWhiteSpace(t.BrokerName)
                        ? (isWd ? "Payout request" : "Rebate credited")
                        : t.BrokerName!,
                    TimeAgo = RelativeTime(t.CreatedAt, now),
                    Amount = t.Amount,
                    IsCredit = !isWd,
                    Status = status,
                    Kind = isWd ? "withdrawal" : "rebate"
                };
            })
            .ToList();

        var model = new DashboardDto
        {
            UserName = user?.FullName ?? User.Identity?.Name,
            Email = user?.Email ?? User.Identity?.Name,
            CashbackBalance = balance,
            TotalEarned = totalEarned,
            TotalWithdrawn = totalWithdrawn,
            PendingWithdrawn = pendingWithdrawn,
            ActiveAccounts = accounts.Count,
            BrokerCount = accounts.Where(a => a.BrokerId.HasValue).Select(a => a.BrokerId).Distinct().Count(),
            WithdrawalCount = debits.Count,
            NotificationCount = txns.Count(t => IsPending(t.Status)),
            YoyPercent = Math.Round(yoy, 0),
            WalletTail = uid == Guid.Empty ? "0000" : uid.ToString("N")[^4..].ToUpperInvariant(),
            ChartMonths = months,
            ChartEarned = earnedSeries,
            ChartWithdrawn = withdrawnSeries,
            RecentActivity = recent
        };

        return View(model);
    }

    private static string RelativeTime(DateTime whenUtc, DateTime nowUtc)
    {
        var span = nowUtc - whenUtc;
        if (span.TotalSeconds < 60) return "Just now";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} min ago";
        if (span.TotalHours < 24) return $"{(int)span.TotalHours} hour{((int)span.TotalHours == 1 ? "" : "s")} ago";
        if (span.TotalDays < 2) return "Yesterday";
        if (span.TotalDays < 30) return $"{(int)span.TotalDays} days ago";
        if (span.TotalDays < 365) return $"{(int)(span.TotalDays / 30)} month{((int)(span.TotalDays / 30) == 1 ? "" : "s")} ago";
        return whenUtc.ToString("dd MMM yyyy");
    }

    public IActionResult Details() => View();

    public async Task<IActionResult> Brokers()
    {
        var accounts = await _tradingAccountService.GetTradingAccounts(CurrentUserId());
        var brokers = await _brokerRepository.GetBrokerAsync();

        var transactions = await _transactionService.GetUserTransactions(CurrentUserGuid());

        var model = new TradingAccountPageDto
        {
            Accounts = accounts,
            Brokers = brokers,
            Transactions = transactions
        };

        return View(model);
    }

    public IActionResult Rebates() => View();

    public async Task<IActionResult> Withdraw()
    {
        var userId = CurrentUserId();
        var user = (await _userDetailService.loadUserAsync(userId))?.data;

        var uid = CurrentUserGuid();
        var banks = await _bankUserRepo.GetAllByUserId(uid);
        var crypto = await _userCryptoRepo.GetByUserId(uid);

        var model = new WithdrawPageDto
        {
            FullName = user?.FullName ?? User.Identity?.Name,
            Email = user?.Email ?? User.Identity?.Name,
            Banks = banks
                .Select(b => new WithdrawBankOptionDto
                {
                    Id = b.Id,
                    BankName = b.BankName,
                    AccountName = b.AccountName,
                    BankAccountNum = b.BankAccountNum
                })
                .ToList(),
            HasCrypto = crypto != null,
            UsdcWallet = crypto?.UsdcWalletNum,
            EthWallet = crypto?.EthWalletNum,
            BtcWallet = crypto?.BtcWalletNum
        };

        return View(model);
    }

    public IActionResult PaymentMethods() => View();

    // ---- Payment Method Part ----
    
    
    [HttpPost]
    public async Task<ResponseDto<ResponseBankPaymentMethod>> AddBankAccountMethod([FromBody] RequestBankPaymentMethod request)
    {
        return await _bankUserService.AddBankUser(request, CurrentUserGuid());
    }

    [HttpGet]
    public async Task<ResponseDto<ResponseBankPaymentMethod>> GetBankPaymentMethod()
    {
        return await _bankUserService.GetBankUser(CurrentUserGuid());
    }

    [HttpPost]
    public async Task<ResponseDto<ResponseCryptoPaymentMethod>> AddCryptoPaymentMethod([FromBody] RequestCryptoPaymentMethod request)
    {
        return await _userCryptoService.AddCryptoUser(request, CurrentUserGuid());
    }

    [HttpGet]
    public async Task<ResponseDto<ResponseCryptoPaymentMethod>> GetCryptoPaymentMethod()
    {
        return await _userCryptoService.GetCryptoUser(CurrentUserGuid());
    }
    
    
    
    
    
    
    

    private string CurrentUserId() =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value ?? string.Empty;

    private Guid CurrentUserGuid() =>
        Guid.TryParse(CurrentUserId(), out var id) ? id : Guid.Empty;
}
