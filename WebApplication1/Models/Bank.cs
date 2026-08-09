using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models;

[Table("tbl_banks", Schema = "public")]
public class Bank
{
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public Guid Id { get; set; }

    [JsonProperty("bank_name")]
    [Column("bank_name")]
    public string BankName { get; set; } = string.Empty;

    [JsonProperty("account_number")]
    [Column("account_number")]
    public string AccountNumber { get; set; } = string.Empty;

    [JsonProperty("account_name")]
    [Column("account_name")]
    public string AccountName { get; set; } = string.Empty;

    [JsonProperty("payment_method")]
    [Column("payment_method")]
    public string? PaymentMethod { get; set; }

    [JsonProperty("currency")]
    [Column("currency")]
    public string Currency { get; set; } = "USD";

    [JsonProperty("status")]
    [Column("status")]
    public string Status { get; set; } = "active";

    [JsonProperty("image_url")]
    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [JsonProperty("qr_image")]
    [Column("qr_image")]
    public string? QrImage { get; set; }

    [JsonProperty("created_by")]
    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [JsonProperty("updated_by")]
    [Column("updated_by")]
    public Guid? UpdatedBy { get; set; }

    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonProperty("average_transaction")]
    [Column("average_transaction", TypeName = "numeric(18,2)")]
    public decimal? AverageTransaction { get; set; }

    [JsonProperty("min_transaction")]
    [Column("min_transaction", TypeName = "numeric(18,2)")]
    public decimal? MinTransaction { get; set; }
}
