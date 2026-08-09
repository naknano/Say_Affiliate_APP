using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models;


[Table("tbl_banks_user", Schema = "public")]
public class UserBank
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonProperty("id")]
    [Column("id")]
    public int Id { get; set; }

    [JsonProperty("user_id")]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [JsonProperty("bank_id")]
    [Column("bank_id")]
    public Guid? BankId { get; set; }

    [JsonProperty("bank_name")]
    [Column("bank_name")]
    public string BankName { get; set; } = string.Empty;

    [JsonProperty("account_name")]
    [Column("account_name")]
    public string AccountName { get; set; } = string.Empty;

    [JsonProperty("bank_account_num")]
    [Column("bank_account_num")]
    public string BankAccountNum { get; set; } = string.Empty;

    [JsonProperty("payment_method")]
    [Column("payment_method")]
    public int? PaymentMethod { get; set; }

    [JsonProperty("is_active")]
    [Column("is_active")]
    public bool IsActive { get; set; }

    [JsonProperty("created_by")]
    [Column("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_by")]
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }

    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
