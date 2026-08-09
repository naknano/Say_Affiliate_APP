namespace WebApplication1.Data.DTO.TradingAccount;

public class UserTradingAccountResponseDto
{
    public Guid Id { get; set; }

    public string TradingAcc { get; set; } = string.Empty;

    public Guid? BrokerId { get; set; }

    public string? BrokerName { get; set; }

    public DateTime? CreatedAt { get; set; }
}
