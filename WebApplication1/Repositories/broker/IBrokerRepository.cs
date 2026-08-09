using WebApplication1.Data.DTO.Broker;
using WebApplication1.Data.DTO.TradingAccount;

namespace WebApplication1.Repositories.broker;

public interface IBrokerRepository
{
    Task<List<BrokerOptionDto>> GetBrokerAsync();

    // full display cards for public broker pages (cached)
    Task<List<BrokerCardDto>> GetBrokerCardsAsync();

    // single broker (served from the cached list)
    Task<BrokerCardDto?> GetBrokerByIdAsync(Guid id);

    // DB-level paged query — fetches only the current page (LIMIT/OFFSET) + counts
    Task<BrokerListPageDto> GetBrokerCardsPagedAsync(string tab, int page, int pageSize);

    // clears the cached broker lists so the next read reloads from the DB
    void InvalidateCache();
}
