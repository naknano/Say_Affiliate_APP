using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Authentication;
using WebApplication1.Data;

namespace WebApplication1.Repositories.Authentication;

public class UserDetailDetailRepositoryImp : IUserDetailRepository
{
    private readonly AppDBContext _dbContext;
    
    public UserDetailDetailRepositoryImp(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDetail> createAsync(UserDetail UserDetail)
    {
        await _dbContext.UserDetails.AddAsync(UserDetail);
        await _dbContext.SaveChangesAsync();
        return UserDetail;
    }
    
    public async Task<UserDetail> updateAsync(UserDetail UserDetail)
    {
        await _dbContext.SaveChangesAsync();
        return UserDetail;
    }

    public async Task<UserDetail?> getByIdAsync(string id)
    {
       return await _dbContext.UserDetails.FirstOrDefaultAsync(x=> x.UserId == id);
    }
}