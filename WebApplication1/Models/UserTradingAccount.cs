using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models;

[Table("tbl_user_trading_account", Schema = "public")]
public class UserTradingAccount
{
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public Guid Id { get; set; }

    [JsonProperty("broker_id")]
    [Column("broker_id")]
    public Guid? BrokerId { get; set; }

    [JsonProperty("user_id")]
    [Column("user_id")]
    public string? UserId { get; set; }

    [JsonProperty("trading_acc")]
    [Column("trading_acc")]
    public string? TradingAcc { get; set; }

    [JsonProperty("created_by")]
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("updated_by")]
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }

    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
