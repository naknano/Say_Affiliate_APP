using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models;

[Table("tbl_transaction_logs", Schema = "public")]
public class TransactionLog
{
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public Guid Id { get; set; }

    [JsonProperty("transaction_id")]
    [Column("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    [JsonProperty("actor_id")]
    [Column("actor_id")]
    public Guid? ActorId { get; set; }

    [JsonProperty("old_status")]
    [Column("old_status")]
    public string? OldStatus { get; set; }

    [JsonProperty("new_status")]
    [Column("new_status")]
    public string? NewStatus { get; set; }

    [JsonProperty("note")]
    [Column("note")]
    public string? Note { get; set; }

    [JsonProperty("logged_at")]
    [Column("logged_at")]
    public DateTime LoggedAt { get; set; }
}
