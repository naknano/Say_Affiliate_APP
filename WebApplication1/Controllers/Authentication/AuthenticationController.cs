using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Authentication;
using WebApplication1.Data.DTO.User;
using WebApplication1.Services.Authentication;

namespace WebApplication1.Controllers.Authentication;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthentication _authenticationService;
    
    public AuthenticationController(
        IAuthentication authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    
    [HttpPost("register")]
    public async Task<ResponseDto<string>> Register([FromBody] UserRegisterRequestDto register)
    {
        return await _authenticationService.RegisterAsync(register);
    }
    
    
    [HttpPost("login")]
    public async Task<ResponseDto<string>> Login([FromBody] LoginRequestDto login)
    {
       return await _authenticationService.LoginAsync(login);
    }
    
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<ResponseDto<string>> Logout()
    {
        return await _authenticationService.LogoutAsync();
    }
    
    [Authorize]
    [HttpPost("change-password")]
    public async Task<ResponseDto<string>> ChangePassword([FromBody] UserChangePasswordRequestDto changePasswordRequest)
    {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? HttpContext.User.FindFirst("sub")?.Value ?? string.Empty;
        return await _authenticationService.ChangePassword(userId,changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword);
    }

    [HttpPost("forgot-password")]
    public async Task<ResponseDto<string>> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
    {
        // Build the reset page URL from the current request host so the emailed link
        // points back to this site's Home/ResetPassword view.
        string resetBaseUrl = $"{Request.Scheme}://{Request.Host}/Home/ResetPassword";
        return await _authenticationService.ForgotPasswordAsync(request.Email, resetBaseUrl);
    }

    [HttpPost("reset-password")]
    public async Task<ResponseDto<string>> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        return await _authenticationService.ResetPasswordAsync(request);
    }

}