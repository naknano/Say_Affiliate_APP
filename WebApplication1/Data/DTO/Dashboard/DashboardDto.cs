namespace WebApplication1.Data.DTO.Dashboard;

public class DashboardDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }

    // ---- headline figures ----
    public decimal CashbackBalance { get; set; }
    public decimal TotalEarned { get; set; }
    public decimal TotalWithdrawn { get; set; }
    public decimal PendingWithdrawn { get; set; }

    public int ActiveAccounts { get; set; }
    public int BrokerCount { get; set; }
    public int WithdrawalCount { get; set; }
    public int NotificationCount { get; set; }

    public double YoyPercent { get; set; }

    // = TotalEarned, shown as the chart's gross figure
    public decimal GrossCashback => TotalEarned;

    // last 4 identifier shown on the wallet card
    public string WalletTail { get; set; } = "0000";

    // ---- chart series (last 8 months) ----
    public List<string> ChartMonths { get; set; } = new();
    public List<decimal> ChartEarned { get; set; } = new();
    public List<decimal> ChartWithdrawn { get; set; } = new();

    // ---- recent activity ----
    public List<DashboardActivityDto> RecentActivity { get; set; } = new();
}

public class DashboardActivityDto
{
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string TimeAgo { get; set; } = "";
    public decimal Amount { get; set; }
    public bool IsCredit { get; set; }
    public string Status { get; set; } = "";   // settled | pending | rejected
    public string Kind { get; set; } = "";     // rebate | withdrawal
}
