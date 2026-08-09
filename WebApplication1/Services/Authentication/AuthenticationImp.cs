using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Repositories.Authentication;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Authentication;
using WebApplication1.Data.DTO.User;
using WebApplication1.Util;

namespace WebApplication1.Services.Authentication;

public class AuthenticationImp : IAuthentication
{
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AppDBContext _appDbContext;
    private readonly IUserDetailService _userDetailService;

    public AuthenticationImp(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        AppDBContext appDbContext,
        IUserDetailService userDetailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appDbContext = appDbContext;
        _userDetailService = userDetailService;
    }

    public async Task<ResponseDto<string>> LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();

            return new ResponseDto<string>
            {
                responseCode = 200,
                responseMessage = "Logout success",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<string>()
            {
                responseCode = 500,
                responseMessage = $"Logout failed: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ResponseDto<string>> LoginAsync(LoginRequestDto login)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded)
            {
                return new ResponseDto<string>()
                {
                    responseCode = Main.code400,
                    responseMessage = "Login failed: Invalid email or password",
                    data = null
                };
            }

            return new ResponseDto<string>()
            {
                responseCode = Main.code200,
                responseMessage = "Login success",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<string>()
            {
                responseCode = Main.code500,
                responseMessage = $"Login failed: {ex.Message}",
                data = null
            };
        }
      
    }

    public async Task<ResponseDto<string>> RegisterAsync(UserRegisterRequestDto register)
    { 
        using var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            var user = new IdentityUser 
            { 
                UserName = register.Email, 
                Email = register.Email  
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return new ResponseDto<string>
                {
                    responseCode = 400,
                    responseMessage = "Register failed",
                    data = string.Join(",", result.Errors.Select(e => e.Description))
                };
            }
            else
            {
                string userId = user.Id; 
                var userDetail = await _userDetailService.createAsync(register.Email, userId);
                if (userDetail.responseCode != 200)
                {
                    // Rollback the user creation if user detail creation fails
                    await transaction.RollbackAsync();
                    return new ResponseDto<string>
                    {
                        responseCode = userDetail.responseCode,
                        responseMessage = userDetail.responseMessage,
                        data = null
                    };
                }
            
            }
            
            // Commit the transaction if both user and user detail creation succeed
            await transaction.CommitAsync();
            return new ResponseDto<string>
            {
                responseCode = 200,
                responseMessage = "User created",
                data = null
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseDto<string>
            {
                responseCode = 500,
                responseMessage = ex.Message,
                data = null
            };
        }
    }

    public async Task<ResponseDto<string>> ChangePassword(string userId,string currentPassword, string newPassword)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto<string>
                {
                    responseCode = 404,
                    responseMessage = "User not found",
                    data = null
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                return new ResponseDto<string>
                {
                    responseCode = 400,
                    responseMessage = string.Join(", ", result.Errors.Select(e => e.Description)),
                    data = null
                };
            }
            
            // If change success need to sign out the user to force them to log in again with the new password
            await _signInManager.SignOutAsync();

            return new ResponseDto<string>
            {
                responseCode = 200,
                responseMessage = "Password changed successfully"
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<string>()
            {
                responseCode = 500,
                responseMessage = $"Change password failed: {ex.Message}",
                data = null
            };
        }
    }
}