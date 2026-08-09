namespace WebApplication1.Data.DTO.Broker;

// Safe, display-only view of a broker for public pages (no api keys / webhooks).
public class BrokerCardDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Rating { get; set; }
    public string? RatingLabel { get; set; }
    public string? Badges { get; set; }
    public string? CashbackOffer { get; set; }
    public string? PaymentSchedule { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? SignupUrl { get; set; }
    public string? Note { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
}
