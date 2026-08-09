using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.User;
using WebApplication1.Models.Authentication;
using WebApplication1.Repositories.Authentication;
using WebApplication1.Util;

namespace WebApplication1.Services.Authentication;

public class UserDetailServiceImp : IUserDetailService
{
    private readonly IUserDetailRepository _userDetailRepository;
    
    public UserDetailServiceImp(IUserDetailRepository userDetailRepository)
    {
        _userDetailRepository = userDetailRepository;
    }
    
    public async Task<ResponseDto<UserDetail>> createAsync (string email, string userId)
    {
        try
        {
            UserDetail userDetail = new UserDetail()
            {
                UserId = userId,
                UserDetailId = Guid.NewGuid().ToString(),
                Email = email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };
            
           var result = await _userDetailRepository.createAsync(userDetail);
           
           if (result == null)
           {
               return new ResponseDto<UserDetail>()
               {
                   responseCode = Main.code400,
                   responseMessage = "Failed to create user detail",
                    data = null
               };
           }
           
           return new ResponseDto<UserDetail>()
           {
               responseCode = Main.code200,
               responseMessage = "Success",
               data = null
           };
        }
        catch (Exception ex)
        {
            return new ResponseDto<UserDetail>()
            {
                responseCode = Main.code500,
                responseMessage = $"An error occurred: {ex.Message}",
                data = null
            };
        }
    }

    public async Task<ResponseDto<UserDetail>> loadUserAsync(string userId)
    {
        try
        {
            var user = await _userDetailRepository.getByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto<UserDetail>()
                {
                    responseCode = Main.code200, // need to throw 200 allow for user to make update data
                    responseMessage = "Failed to find user detail",
                    data = null
                };
            }

            return new ResponseDto<UserDetail>()
            {
                responseCode = Main.code200,
                responseMessage = "Success",
                data =  user
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<UserDetail>()
            {
                responseCode = Main.code500,
                responseMessage = $"An error occurred: {ex.Message}",
                data = null
            };
        }
    }


    public async Task<ResponseDto<UserDetail>> updateAsync(UserUpdateRequestDto useUpdate, string userId)
    {
        try
        {
            var user = await _userDetailRepository.getByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto<UserDetail>()
                {
                    responseCode = Main.code400,
                    responseMessage = "Failed to find user detail",
                    data = null
                };
            }
            
            user.Email = useUpdate.Email;
            user.FullName = useUpdate.FullName;
            user.Address = useUpdate.Address;
            user.Phone = useUpdate.Phone;
            user.Telegram = useUpdate.Telegram;
            user.Whatapp = useUpdate.Whatapp;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = userId;

            var result = await _userDetailRepository.updateAsync(user);
            if (result == null)
            {
                return new ResponseDto<UserDetail>()
                {
                    responseCode = Main.code400,
                    responseMessage = "Failed to update user detail",
                    data = null
                };
            }
            
            return new ResponseDto<UserDetail>()
            {
                responseCode = Main.code200,
                responseMessage = "Success",
                data = user
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<UserDetail>()
            {
                responseCode = Main.code500,
                responseMessage = $"An error occurred: {ex.Message}",
                data = null
            };
        }
    }
    
}