using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models.Content;

[Table("tbl_broker", Schema = "public")]
public class Broker
{
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public Guid Id { get; set; }

    [JsonProperty("name")]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("api_key_hash")]
    [Column("api_key_hash")]
    public string? ApiKeyHash { get; set; }

    [JsonProperty("webhook_url")]
    [Column("webhook_url")]
    public string? WebhookUrl { get; set; }

    [JsonProperty("status")]
    [Column("status")]
    public string? Status { get; set; } = "active";

    [JsonProperty("image_url")]
    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("created_by")]
    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [JsonProperty("updated_by")]
    [Column("updated_by")]
    public Guid? UpdatedBy { get; set; }

    [JsonProperty("api_key")]
    [Column("api_key")]
    public string? ApiKey { get; set; }

    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonProperty("email")]
    [Column("email")]
    public string? Email { get; set; }

    [JsonProperty("note")]
    [Column("note")]
    public string? Note { get; set; }

    [JsonProperty("website_url")]
    [Column("website_url")]
    public string? WebsiteUrl { get; set; }

    [JsonProperty("signup_url")]
    [Column("signup_url")]
    public string? SignupUrl { get; set; }

    [JsonProperty("description")]
    [Column("description")]
    public string? Description { get; set; }

    [JsonProperty("cashback_offer")]
    [Column("cashback_offer")]
    public string? CashbackOffer { get; set; }

    [JsonProperty("payment_schedule")]
    [Column("payment_schedule")]
    public string? PaymentSchedule { get; set; }

    [JsonProperty("rating")]
    [Column("rating", TypeName = "numeric(3,1)")]
    public decimal? Rating { get; set; }

    [JsonProperty("rating_label")]
    [Column("rating_label")]
    public string? RatingLabel { get; set; }

    [JsonProperty("badges")]
    [Column("badges")]
    public string? Badges { get; set; }

    [JsonProperty("source")]
    [Column("source")]
    public string? Source { get; set; } = "artisgain.com";
}
