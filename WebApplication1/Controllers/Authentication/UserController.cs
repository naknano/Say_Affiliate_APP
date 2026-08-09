using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.User;
using WebApplication1.Models.Authentication;
using WebApplication1.Services.Authentication;

namespace WebApplication1.Controllers;



[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserDetailService _userDetailService;

    public UserController(IUserDetailService userDetailService)
    {
        _userDetailService = userDetailService;
    }
    
    [Authorize]
    [HttpGet("load-user")]
    public async Task<ResponseDto<UserDetail>> loadUserAsync()
    {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? HttpContext.User.FindFirst("sub")?.Value ?? string.Empty;
        return await _userDetailService.loadUserAsync(userId);
    }

    [Authorize]
    [HttpPost("update-user")]
    public async Task<ResponseDto<UserDetail>> updateUserAsync(UserUpdateRequestDto userUpdate)
    {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? HttpContext.User.FindFirst("sub")?.Value ?? string.Empty;
        return await _userDetailService.updateAsync(userUpdate, userId);
    }

}