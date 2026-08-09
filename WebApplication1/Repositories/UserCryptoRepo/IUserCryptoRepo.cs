using WebApplication1.Models;

namespace WebApplication1.Repositories.UserCryptoRepo;

public interface IUserCryptoRepo
{
    Task<UserCrypto> AddUserCrpyto(UserCrypto userCrypto);
    Task<UserCrypto?> GetUserCrpyto(Guid id);
    Task<UserCrypto?> GetByUserId(Guid userId);
    Task<UserCrypto> UpdateUserCrpyto(UserCrypto userCrypto);
}