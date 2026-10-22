using WebApplication1.Models;

namespace WebApplication1.Repositories.BankUserRepo;

public interface IBankUserRepo
{
    Task<UserBank?> GetByUserId(Guid userId);
    Task<List<UserBank>> GetAllByUserId(Guid userId);
    Task<UserBank?> GetByIdAndUser(int id, Guid userId);

    // tbl_banks (the bank record linked from tbl_banks_user.bank_id)
    Task<Bank?> GetBankById(Guid id);

    // Persist the bank and its user mapping together in one atomic save.
    Task<bool> SaveBankAndMappingAsync(Bank bank, bool bankIsNew, UserBank userBank, bool userBankIsNew);
}
