namespace WebApplication1.Data.DTO.Broker;

public class BrokerListPageDto
{
    public List<BrokerCardDto> Items { get; set; } = new();

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages { get; set; } = 1;

    public string Tab { get; set; } = "all";  // "all" | "top"
    public int AllCount { get; set; }
    public int TopCount { get; set; }
}
