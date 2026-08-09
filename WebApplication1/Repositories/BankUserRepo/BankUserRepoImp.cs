using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories.BankUserRepo;

public class BankUserRepoImp : IBankUserRepo
{
    private readonly AppDBContext _context;

    public BankUserRepoImp(AppDBContext context)
    {
        _context = context;
    }

    public async Task<UserBank?> AddUserBank(UserBank userBank)
    {
        try
        {
            await _context.UserBanks.AddAsync(userBank);
            await _context.SaveChangesAsync();
            return userBank;
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserBank?> GetByUserId(Guid userId)
    {
        return await _context.UserBanks.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<List<UserBank>> GetAllByUserId(Guid userId)
    {
        return await _context.UserBanks
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserBank?> GetByIdAndUser(int id, Guid userId)
    {
        return await _context.UserBanks
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }

    public async Task<UserBank> UpdateUserBank(UserBank userBank)
    {
        _context.UserBanks.Update(userBank);
        await _context.SaveChangesAsync();
        return userBank;
    }
}
