using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Repositories.Authentication;
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Authentication;
using WebApplication1.Data.DTO.User;
using WebApplication1.Services.Email;
using WebApplication1.Util;
using System.Text.Encodings.Web;

namespace WebApplication1.Services.Authentication;

public class AuthenticationImp : IAuthentication
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AppDBContext _appDbContext;
    private readonly IUserDetailService _userDetailService;
    private readonly IEmailSender _emailSender;

    public AuthenticationImp(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        AppDBContext appDbContext,
        IUserDetailService userDetailService,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appDbContext = appDbContext;
        _userDetailService = userDetailService;
        _emailSender = emailSender;
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

    public async Task<ResponseDto<string>> ForgotPasswordAsync(string email, string resetBaseUrl)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);

            // Do not reveal whether the account exists — always report success.
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Token is URL-encoded so it survives the query string round-trip.
                var resetLink =
                    $"{resetBaseUrl}?email={UrlEncoder.Default.Encode(email)}&token={UrlEncoder.Default.Encode(token)}";

                var htmlBody =
                    $@"<p>Hi,</p>
                       <p>We received a request to reset the password for your SAG account.</p>
                       <p>Click the link below to set a new password. This link can only be used once.</p>
                       <p><a href=""{resetLink}"">Reset your password</a></p>
                       <p>If you did not request this, you can safely ignore this email.</p>
                       <p>Thanks,<br/>SAGPay System</p>";

                await _emailSender.SendEmailAsync(email, "Reset your password", htmlBody);
            }

            return new ResponseDto<string>
            {
                responseCode = Main.code200,
                responseMessage = "If an account exists for that email, a reset link has been sent.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<string>
            {
                responseCode = Main.code500,
                responseMessage = $"Forgot password failed: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ResponseDto<string>> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Same generic message to avoid account enumeration.
                return new ResponseDto<string>
                {
                    responseCode = Main.code400,
                    responseMessage = "Invalid or expired reset link.",
                    data = null
                };
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResponseDto<string>
                {
                    responseCode = Main.code400,
                    responseMessage = string.Join(", ", result.Errors.Select(e => e.Description)),
                    data = null
                };
            }

            return new ResponseDto<string>
            {
                responseCode = Main.code200,
                responseMessage = "Password has been reset successfully.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<string>
            {
                responseCode = Main.code500,
                responseMessage = $"Reset password failed: {ex.Message}",
                data = null
            };
        }
    }
}