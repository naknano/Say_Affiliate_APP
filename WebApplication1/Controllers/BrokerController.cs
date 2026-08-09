using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;
using WebApplication1.Repositories.broker;
using WebApplication1.Services.Authentication;
using WebApplication1.Services.TransactionService;

namespace WebApplication1.Controllers;

[Authorize]
public class BrokerController : Controller
{
    private readonly ITradingAccount _tradingAccountService;
    private readonly IBrokerRepository _brokerRepository;
    private readonly ITransactionService _transactionService;

    public BrokerController(ITradingAccount tradingAccountService, IBrokerRepository brokerRepository, ITransactionService transactionService)
    {
        _tradingAccountService = tradingAccountService;
        _brokerRepository = brokerRepository;
        _transactionService = transactionService;
    }

    // ---- data used by the "Add Trading Account" form ----
    [HttpGet]
    public async Task<ResponseDto<BrokerOptionDto>> GetBrokers()
    {
        return new ResponseDto<BrokerOptionDto>
        {
            responseCode = 200,
            responseMessage = "Success",
            dataList = await _brokerRepository.GetBrokerAsync()
        };
    }

    [HttpGet]
    public async Task<List<UserTradingAccountResponseDto>> GetTradingAccounts()
    {
        return await _tradingAccountService.GetTradingAccounts(CurrentUserId());
    }

    [HttpGet]
    public async Task<List<TransactionListItemDto>> GetTransactions()
    {
        return await _transactionService.GetUserTransactions(CurrentUserGuid());
    }

    // ---- trading account management ----
    [HttpPost]
    public async Task<ResponseDto<UserTradingAccount>> AddTradingAccount([FromBody] UserTradingAccountRequestDto request)
    {
        return await _tradingAccountService.AddTradingAccount(request, CurrentUserId());
    }

    [HttpPost]
    public async Task<ResponseDto<UserTradingAccount>> UpdateTradingAccount([FromBody] UserTradingAccountUpdateDto request)
    {
        return await _tradingAccountService.UpdateTradingAccount(request, CurrentUserId());
    }

    [HttpPost]
    public async Task<ResponseDto<UserTradingAccount>> DeleteTradingAccount([FromBody] UserTradingAccountUpdateDto request)
    {
        return await _tradingAccountService.DeleteTradingAccount(request?.Id ?? string.Empty, CurrentUserId());
    }

    private string CurrentUserId() =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value ?? string.Empty;

    private Guid CurrentUserGuid() =>
        Guid.TryParse(CurrentUserId(), out var id) ? id : Guid.Empty;
}
