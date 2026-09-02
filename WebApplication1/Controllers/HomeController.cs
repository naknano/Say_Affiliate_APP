using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories.broker;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBrokerRepository _brokerRepository;

    public HomeController(ILogger<HomeController> logger, IBrokerRepository brokerRepository)
    {
        _logger = logger;
        _brokerRepository = brokerRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    
    public async Task<IActionResult> Broker(string tab = "all", int page = 1)
    {
        // fetches only the current page (10 rows) straight from the DB — the full list is never loaded
        var model = await _brokerRepository.GetBrokerCardsPagedAsync(tab, page, pageSize: 10);
        return View(model);
    }

    public async Task<IActionResult> BrokerDetail(Guid? id)
    {
        if (id is null || id == Guid.Empty)
            return RedirectToAction(nameof(Broker));

        var broker = await _brokerRepository.GetBrokerByIdAsync(id.Value);
        if (broker is null)
            return RedirectToAction(nameof(Broker));

        var related = (await _brokerRepository.GetBrokerCardsAsync())
            .Where(b => b.Id != broker.Id)
            .OrderByDescending(b => b.Rating)
            .Take(4)
            .ToList();

        return View(new WebApplication1.Data.DTO.Broker.BrokerDetailPageDto
        {
            Broker = broker,
            Related = related
        });
    }

    public async Task<IActionResult> SetupRebates(Guid? id)
    {
        if (id is null || id == Guid.Empty)
            return RedirectToAction(nameof(Broker));

        var broker = await _brokerRepository.GetBrokerByIdAsync(id.Value);
        if (broker is null)
            return RedirectToAction(nameof(Broker));

        return View(broker);
    }

    [Authorize]
    public IActionResult Profile()
    {
        // Profile is now a multi-page dashboard under ProfileController.
        return RedirectToAction("Dashboard", "Profile");
    }
    
    public IActionResult ContactUs()
    {
        return View();
    }

    public IActionResult FAQs()
    {
        return View();
    }
    
    public IActionResult GetAdvice()
    {
        return View();
    }

    public IActionResult ComingSoon()
    {
        return View();
    }

    public IActionResult LotSizeCalculator()
    {
        return View();
    }

    public IActionResult PipValueCalculator()
    {
        return View();
    }

    public IActionResult ForexCaculator()
    {
        return View();
    }

    public IActionResult LiveBrokerSpreads()
    {
        return View();
    }

    public IActionResult Economic()
    {
        return View();
    }

    public IActionResult CrossRates()
    {
        return View();
    }

    public IActionResult HeatMap()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}