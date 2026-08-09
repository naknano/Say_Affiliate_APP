using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Withdraw;
using WebApplication1.Services.WithdrawService;

namespace WebApplication1.Controllers;

[Authorize]
public class WithdrawController : Controller
{
    private readonly IWithdrawService _withdrawService;

    public WithdrawController(IWithdrawService withdrawService)
    {
        _withdrawService = withdrawService;
    }

    // create a pending withdrawal transaction
    [HttpPost]
    public async Task<ResponseDto<string>> PostWithdrawTransaction([FromBody] RequestWithdrawDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? User.FindFirst("sub")?.Value
                     ?? string.Empty;
        var email = User.Identity?.Name ?? string.Empty;

        return await _withdrawService.PostWithdraw(request, userId, email);
    }
}
