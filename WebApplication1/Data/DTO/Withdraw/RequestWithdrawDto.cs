namespace WebApplication1.Data.DTO.Withdraw;

public class RequestWithdrawDto
{
    public decimal Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    // "bank" or "crypto"
    public string Method { get; set; } = string.Empty;

    // when Method = "bank": the selected user bank record id
    public string UserBankId { get; set; } = string.Empty;

    // when Method = "crypto": USDC | ETH | BTC
    public string CryptoNetwork { get; set; } = string.Empty;
}
