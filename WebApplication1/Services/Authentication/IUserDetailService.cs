using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.User;
using WebApplication1.Models.Authentication;

namespace WebApplication1.Services.Authentication;

public interface IUserDetailService
{
    public Task<ResponseDto<UserDetail>> createAsync (string email, string userId);
    
    public Task<ResponseDto<UserDetail>> loadUserAsync (string userId);
    
    public Task<ResponseDto<UserDetail>> updateAsync (UserUpdateRequestDto useUpdate, string userId);
    
}