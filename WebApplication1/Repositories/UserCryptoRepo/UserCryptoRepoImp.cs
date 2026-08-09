using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories.UserCryptoRepo;

public class UserCryptoRepoImp : IUserCryptoRepo
{
    private readonly AppDBContext _context;

    public UserCryptoRepoImp(AppDBContext context)
    {
        _context = context;
    }
    
    public async Task<UserCrypto> AddUserCrpyto(UserCrypto userCrypto)
    {
        await _context.UserCryptos.AddAsync(userCrypto);
        await _context.SaveChangesAsync();
        return userCrypto;
    }

    public async Task<UserCrypto?> GetUserCrpyto(Guid id)
    {
        return await _context.UserCryptos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserCrypto?> GetByUserId(Guid userId)
    {
        return await _context.UserCryptos.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<UserCrypto> UpdateUserCrpyto(UserCrypto userCrypto)
    {
        _context.UserCryptos.Update(userCrypto);
        await _context.SaveChangesAsync();
        return userCrypto;
    }
}