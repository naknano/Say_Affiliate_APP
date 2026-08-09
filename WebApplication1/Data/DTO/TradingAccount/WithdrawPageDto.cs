namespace WebApplication1.Data.DTO.TradingAccount;

public class WithdrawPageDto
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public List<WithdrawBankOptionDto> Banks { get; set; } = new();

    // crypto wallets (from the user's saved crypto payment method)
    public bool HasCrypto { get; set; }

    public string? UsdcWallet { get; set; }

    public string? EthWallet { get; set; }

    public string? BtcWallet { get; set; }
}

public class WithdrawBankOptionDto
{
    public int Id { get; set; }

    public string? BankName { get; set; }

    public string? AccountName { get; set; }

    public string? BankAccountNum { get; set; }
}
