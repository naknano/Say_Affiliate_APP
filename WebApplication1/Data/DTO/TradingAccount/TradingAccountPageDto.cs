namespace WebApplication1.Data.DTO.TradingAccount;

public class TradingAccountPageDto
{
    public List<UserTradingAccountResponseDto> Accounts { get; set; } = new();

    public List<BrokerOptionDto> Brokers { get; set; } = new();

    public List<TransactionListItemDto> Transactions { get; set; } = new();
}

public class BrokerOptionDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Note { get; set; }

    public string? ImageUrl { get; set; }
}
