using System.Text.Json.Serialization;

namespace WebApplication1.Data.DTO.PaymentMethod;

public class RequestCryptoPaymentMethod
{
    [JsonPropertyName("usdc_wallet")]
    public string UsdcWallet { get; set; } = string.Empty;

    [JsonPropertyName("eth_wallet")]
    public string EthWallet { get; set; } = string.Empty;

    [JsonPropertyName("btc_wallet")]
    public string BtcWallet { get; set; } = string.Empty;
}
