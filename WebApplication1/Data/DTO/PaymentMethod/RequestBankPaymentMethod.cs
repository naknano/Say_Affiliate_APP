using System.Text.Json.Serialization;

namespace WebApplication1.Data.DTO.PaymentMethod;

public class RequestBankPaymentMethod
{       
    [JsonPropertyName("bank_id")]
    public string BankId { get; set; } = string.Empty;

    [JsonPropertyName("bank_name")]
    public string BankName { get; set; } = string.Empty;
    
    [JsonPropertyName("account_name")]
    public string AccountName { get; set; } = string.Empty;
    
    [JsonPropertyName("bank_account_num")]
    public string BankAccountNum { get; set; } = string.Empty;
}