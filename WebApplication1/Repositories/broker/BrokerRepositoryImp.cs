using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.Data;
using WebApplication1.Data.DTO.Broker;
using WebApplication1.Data.DTO.TradingAccount;

namespace WebApplication1.Repositories.broker;

public class BrokerRepositoryImp : IBrokerRepository
{
    private readonly AppDBContext _dbContext;
    private readonly IMemoryCache _cache;

    // brokers are shared reference data that changes rarely — cache the list
    private const string BrokerCacheKey = "brokers:all";
    private const string BrokerCardsCacheKey = "brokers:cards";
    private static readonly TimeSpan BrokerCacheTtl = TimeSpan.FromMinutes(10);

    public BrokerRepositoryImp(AppDBContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<List<BrokerOptionDto>> GetBrokerAsync()
    {
        return await _cache.GetOrCreateAsync(BrokerCacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = BrokerCacheTtl;

            return _dbContext.brokers
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .Select(b => new BrokerOptionDto { Id = b.Id, Name = b.Name, Note = b.Note, ImageUrl = b.ImageUrl })
                .ToListAsync();
        }) ?? new List<BrokerOptionDto>();
    }

    public async Task<List<BrokerCardDto>> GetBrokerCardsAsync()
    {
        return await _cache.GetOrCreateAsync(BrokerCardsCacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = BrokerCacheTtl;

            return _dbContext.brokers
                .AsNoTracking()
                .OrderByDescending(b => b.Rating)
                .ThenBy(b => b.Name)
                .Select(b => new BrokerCardDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    RatingLabel = b.RatingLabel,
                    Badges = b.Badges,
                    CashbackOffer = b.CashbackOffer,
                    PaymentSchedule = b.PaymentSchedule,
                    WebsiteUrl = b.WebsiteUrl,
                    SignupUrl = b.SignupUrl,
                    Note = b.Note,
                    Description = b.Description,
                    Status = b.Status
                })
                .ToListAsync();
        }) ?? new List<BrokerCardDto>();
    }

    public async Task<BrokerCardDto?> GetBrokerByIdAsync(Guid id)
    {
        // reuse the cached list — no extra DB round trip
        return (await GetBrokerCardsAsync()).FirstOrDefault(b => b.Id == id);
    }

    public async Task<BrokerListPageDto> GetBrokerCardsPagedAsync(string tab, int page, int pageSize)
    {
        tab = string.Equals(tab, "top", StringComparison.OrdinalIgnoreCase) ? "top" : "all";
        if (pageSize < 1) pageSize = 10;

        var baseQuery = _dbContext.brokers.AsNoTracking();

        // "top" = high rating OR a badge containing "top" (evaluated in SQL)
        var topQuery = baseQuery.Where(b =>
            b.Rating >= 4.7m || (b.Badges != null && EF.Functions.ILike(b.Badges, "%top%")));

        // counts run as lightweight COUNT(*) queries
        var allCount = await baseQuery.CountAsync();
        var topCount = await topQuery.CountAsync();

        var filtered = tab == "top" ? topQuery : baseQuery;
        var totalItems = tab == "top" ? topCount : allCount;

        var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)pageSize));
        if (page < 1) page = 1;
        if (page > totalPages) page = totalPages;

        // only 10 rows leave the database (OFFSET/LIMIT)
        var items = await filtered
            .OrderByDescending(b => b.Rating)
            .ThenBy(b => b.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BrokerCardDto
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                Rating = b.Rating,
                RatingLabel = b.RatingLabel,
                Badges = b.Badges,
                CashbackOffer = b.CashbackOffer,
                PaymentSchedule = b.PaymentSchedule,
                WebsiteUrl = b.WebsiteUrl,
                SignupUrl = b.SignupUrl,
                Note = b.Note,
                Description = b.Description,
                Status = b.Status
            })
            .ToListAsync();

        return new BrokerListPageDto
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Tab = tab,
            AllCount = allCount,
            TopCount = topCount
        };
    }

    public void InvalidateCache()
    {
        _cache.Remove(BrokerCacheKey);
        _cache.Remove(BrokerCardsCacheKey);
    }
}
