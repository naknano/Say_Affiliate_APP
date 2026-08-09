using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories.broker;

namespace WebApplication1.Controllers;

// Lets an external admin/tool bust the broker cache after it writes to tbl_broker.
// Server-to-server, so it's protected by a shared secret header instead of a login cookie.
[AllowAnonymous]
public class BrokerCacheController : Controller
{
    private readonly IBrokerRepository _brokerRepository;
    private readonly IConfiguration _config;

    public BrokerCacheController(IBrokerRepository brokerRepository, IConfiguration config)
    {
        _brokerRepository = brokerRepository;
        _config = config;
    }

    // POST /BrokerCache/Refresh
    // Header:  X-Refresh-Key: <value of BrokerCache:RefreshKey>
    [HttpPost]
    public IActionResult Refresh([FromHeader(Name = "X-Refresh-Key")] string? key)
    {
        var expected = _config["BrokerCache:RefreshKey"];

        if (string.IsNullOrWhiteSpace(expected))
            return StatusCode(500, new { responseCode = 500, responseMessage = "Refresh key is not configured." });

        if (string.IsNullOrWhiteSpace(key) || !string.Equals(key, expected, StringComparison.Ordinal))
            return Unauthorized(new { responseCode = 401, responseMessage = "Invalid or missing refresh key." });

        _brokerRepository.InvalidateCache();
        return Ok(new { responseCode = 200, responseMessage = "Broker cache cleared. Next request reloads from the database." });
    }
}
