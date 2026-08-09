using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models;

[Table("tbl_transactions", Schema = "public")]
public class Transaction
{
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("user_id")]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [JsonProperty("bank_id")]
    [Column("bank_id")]
    public Guid? BankId { get; set; }

    [JsonProperty("processed_by")]
    [Column("processed_by")]
    public Guid? ProcessedBy { get; set; }

    [JsonProperty("merchant_transaction_id")]
    [Column("merchant_transaction_id")]
    public string MerchantTransactionId { get; set; } = string.Empty;

    [JsonProperty("transaction_type")]
    [Column("transaction_type")]
    public string TransactionType { get; set; } = string.Empty;

    [JsonProperty("status")]
    [Column("status")]
    public string Status { get; set; } = "pending";

    [JsonProperty("amount")]
    [Column("amount", TypeName = "numeric(18,2)")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    [Column("currency")]
    public string Currency { get; set; } = "USD";

    [JsonProperty("merchant_description")]
    [Column("merchant_description")]
    public string? MerchantDescription { get; set; }

    [JsonProperty("payslip_url")]
    [Column("payslip_url")]
    public string? PayslipUrl { get; set; }

    [JsonProperty("transaction_ref")]
    [Column("transaction_ref")]
    public string? TransactionRef { get; set; }

    [JsonProperty("merchant_status_ack")]
    [Column("merchant_status_ack")]
    public string? MerchantStatusAck { get; set; }

    [JsonProperty("processed_at")]
    [Column("processed_at")]
    public DateTime? ProcessedAt { get; set; }

    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("customer_email")]
    [Column("customer_email")]
    public string? CustomerEmail { get; set; }

    [JsonProperty("customer_country_code")]
    [Column("customer_country_code")]
    public string? CustomerCountryCode { get; set; }

    [JsonProperty("customer_bank_account_name")]
    [Column("customer_bank_account_name")]
    public string? CustomerBankAccountName { get; set; }

    [JsonProperty("customer_bank_account_num")]
    [Column("customer_bank_account_num")]
    public string? CustomerBankAccountNum { get; set; }

    [JsonProperty("sag_bank_account_name")]
    [Column("sag_bank_account_name")]
    public string? SagBankAccountName { get; set; }

    [JsonProperty("sag_bank_account_num")]
    [Column("sag_bank_account_num")]
    public string? SagBankAccountNum { get; set; }
}
