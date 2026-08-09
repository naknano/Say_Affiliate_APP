using System.Text.Json.Serialization;

namespace WebApplication1.Data.DTO.PaymentMethod;

public class ResponseCryptoPaymentMethod
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("usdc_wallet_num")]
    public string UsdcWalletNum { get; set; } = string.Empty;

    [JsonPropertyName("eth_wallet_num")]
    public string EthWalletNum { get; set; } = string.Empty;

    [JsonPropertyName("btc_wallet_num")]
    public string BtcWalletNum { get; set; } = string.Empty;

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}
