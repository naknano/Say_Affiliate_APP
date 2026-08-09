namespace WebApplication1.Data.DTO.Broker;

public class BrokerDetailPageDto
{
    public BrokerCardDto Broker { get; set; } = new();
    public List<BrokerCardDto> Related { get; set; } = new();
}
