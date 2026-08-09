using System.Text.Json.Serialization;

namespace WebApplication1.Data.DTO.PaymentMethod;

public class ResponseBankPaymentMethod
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("bank_id")]
    public Guid BankId { get; set; }

    [JsonPropertyName("bank_name")]
    public string BankName { get; set; } = string.Empty;

    [JsonPropertyName("account_name")]
    public string AccountName { get; set; } = string.Empty;

    [JsonPropertyName("bank_account_num")]
    public string BankAccountNum { get; set; } = string.Empty;

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}
