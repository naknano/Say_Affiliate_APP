using WebApplication1.Models;

namespace WebApplication1.Repositories.BankUserRepo;

public interface IBankUserRepo
{
    Task<UserBank?> AddUserBank(UserBank userBank);
    Task<UserBank?> GetByUserId(Guid userId);
    Task<List<UserBank>> GetAllByUserId(Guid userId);
    Task<UserBank?> GetByIdAndUser(int id, Guid userId);
    Task<UserBank> UpdateUserBank(UserBank userBank);
}
