using WebApplication1.Models.Authentication;

namespace WebApplication1.Repositories.Authentication;

public interface IUserDetailRepository
{
    
    public Task<UserDetail> createAsync(UserDetail user);
    public Task<UserDetail> updateAsync(UserDetail user);
    
    public Task<UserDetail?> getByIdAsync(string id);
}