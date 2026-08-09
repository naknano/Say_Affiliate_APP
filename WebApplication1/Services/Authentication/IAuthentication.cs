
using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Authentication;
using WebApplication1.Data.DTO.User;
using WebApplication1.Models.Authentication;

namespace WebApplication1.Services.Authentication;

public interface IAuthentication
{
    public Task<ResponseDto<string>> LogoutAsync();
    
    public Task<ResponseDto<string>> LoginAsync(LoginRequestDto login);

    public Task<ResponseDto<string>> RegisterAsync(UserRegisterRequestDto register);
    
    public Task<ResponseDto<string>> ChangePassword(string userId ,string currentPassword, string newPassword);
}