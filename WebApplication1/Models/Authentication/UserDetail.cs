using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models.Authentication;

[Table("tbl_user_detail", Schema = "public")]
public class UserDetail
{
   [Key]
    [JsonProperty("user_detail_id")]
    [Column("user_detail_id")]
    public string UserDetailId { get; set; } = string.Empty;

    [JsonProperty("user_id")]
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;
    
    [JsonProperty("full_name")]
    [Column("full_name")]
    public string? FullName { get; set; }
    
    [JsonProperty("first_name")]
    [Column("first_name")]
    public string? FirstName { get; set; }

    [JsonProperty("last_name")]
    [Column("last_name")]
    public string? LastName { get; set; }

    [JsonProperty("email")]
    [Column("email")]
    public string? Email { get; set; }

    [JsonProperty("address")]
    [Column("address")]
    public string? Address { get; set; }

    [JsonProperty("address_2")]
    [Column("address_2")]
    public string? Address2 { get; set; }

    [JsonProperty("phone")]
    [Column("phone")]
    public string? Phone { get; set; }

    [JsonProperty("phone_2")]
    [Column("phone_2")]
    public string? Phone2 { get; set; }

    [JsonProperty("telegram")]
    [Column("telegram")]
    public string? Telegram { get; set; }

    [JsonProperty("whatapp")]
    [Column("whatapp")]
    public string? Whatapp { get; set; }

    [JsonProperty("broker_id")]
    [Column("broker_id")]
    public string? BrokerId { get; set; }

    [JsonProperty("country_code")]
    [Column("country_code")]
    public string? CountryCode { get; set; }

    [JsonProperty("street_no")]
    [Column("street_no")]
    public string? StreetNo { get; set; }

    [JsonProperty("village")]
    [Column("village")]
    public string? Village { get; set; }

    [JsonProperty("province")]
    [Column("province")]
    public string? Province { get; set; }

    [JsonProperty("district")]
    [Column("district")]
    public string? District { get; set; }

    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("created_by")]
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonProperty("updated_by")]
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }

    [JsonProperty("gender")]
    [Column("gender")]
    public string? Gender { get; set; }

}