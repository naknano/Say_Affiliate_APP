using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models;


[Table("tbl_crypto_user", Schema = "public")]
public class UserCrypto
{
    
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("user_id")]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [JsonProperty("usdc_wallet_num")]
    [Column("usdc_wallet_num")]
    public string UsdcWalletNum { get; set; } = string.Empty;
    
    [JsonProperty("eth_wallet_num")]
    [Column("eth_wallet_num")]
    public string EthWalletNum { get; set; } = string.Empty;
    
    [JsonProperty("btc_wallet_num")]
    [Column("btc_wallet_num")]
    public string BtcWalletNum { get; set; } = string.Empty;
    
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
    public string UpdatedBy { get; set; } = string.Empty;
    
    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    
}