namespace WebApplication1.Data.DTO.TradingAccount;

public class TransactionListItemDto
{
    public string Id { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string? BrokerName { get; set; }

    public string? TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Currency { get; set; }

    public string? Status { get; set; }
}
