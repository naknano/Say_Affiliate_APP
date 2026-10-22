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

    public async Task<Bank?> GetBankById(Guid id)
    {
        return await _context.Banks.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveBankAndMappingAsync(Bank bank, bool bankIsNew, UserBank userBank, bool userBankIsNew)
    {
        try
        {
            if (bankIsNew) await _context.Banks.AddAsync(bank);
            else _context.Banks.Update(bank);

            if (userBankIsNew) await _context.UserBanks.AddAsync(userBank);
            else _context.UserBanks.Update(userBank);

            // One SaveChanges => both rows commit (or roll back) together.
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
