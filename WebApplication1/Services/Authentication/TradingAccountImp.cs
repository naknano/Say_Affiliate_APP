using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.TradingAccount;
using WebApplication1.Models;
using WebApplication1.Repositories.TradingAccount;
using WebApplication1.Util;

namespace WebApplication1.Services.Authentication;

public class TradingAccountImp : ITradingAccount
{
    private readonly IUserTradingAccountRepo _userTradingAccountRepo;

    public TradingAccountImp(IUserTradingAccountRepo userTradingAccountRepo)
    {
        _userTradingAccountRepo = userTradingAccountRepo;
    }

    public async Task<ResponseDto<UserTradingAccount>> AddTradingAccount(UserTradingAccountRequestDto request, string userId)
    {
        try
        {
            // ---- validate ----
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Fail(Main.code400, "Your session has expired. Please sign in again.");
            }

            if (string.IsNullOrWhiteSpace(request.TradingAcc))
            {
                return Fail(Main.code400, "Trading account number is required.");
            }

            var tradingAcc = request.TradingAcc.Trim();

            // optional broker id (only bind when a valid GUID is supplied)
            Guid? brokerId = Guid.TryParse(request.BrokerId, out var parsedBrokerId)
                ? parsedBrokerId
                : null;

            // ---- prevent duplicates (cheap indexed lookup) ----
            if (await _userTradingAccountRepo.ExistsAsync(userId, tradingAcc))
            {
                return Fail(Main.code400, "This trading account is already linked to your profile.");
            }

            // ---- persist ----
            var entity = new UserTradingAccount
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BrokerId = brokerId,
                TradingAcc = tradingAcc,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _userTradingAccountRepo.AddUserTradingAccount(entity);
            if (created is null)
            {
                return Fail(Main.code400, "Failed to add trading account.");
            }

            return new ResponseDto<UserTradingAccount>
            {
                responseCode = Main.code200,
                responseMessage = "Trading account added successfully.",
                data = created
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while adding the trading account: {ex.Message}");
        }
    }

    public Task<List<UserTradingAccountResponseDto>> GetTradingAccounts(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Task.FromResult(new List<UserTradingAccountResponseDto>());
        }

        return _userTradingAccountRepo.GetByUserIdAsync(userId);
    }

    public async Task<ResponseDto<UserTradingAccount>> UpdateTradingAccount(UserTradingAccountUpdateDto request, string userId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Fail(Main.code400, "Your session has expired. Please sign in again.");
            }

            if (request is null || !Guid.TryParse(request.Id, out var id))
            {
                return Fail(Main.code400, "Invalid trading account.");
            }

            if (string.IsNullOrWhiteSpace(request.TradingAcc))
            {
                return Fail(Main.code400, "Trading account number is required.");
            }

            var entity = await _userTradingAccountRepo.GetEntityAsync(id, userId);
            if (entity is null)
            {
                return Fail(Main.code400, "Trading account not found.");
            }

            var tradingAcc = request.TradingAcc.Trim();

            // only check for duplicates when the number actually changed
            if (!string.Equals(entity.TradingAcc, tradingAcc, StringComparison.OrdinalIgnoreCase)
                && await _userTradingAccountRepo.ExistsAsync(userId, tradingAcc))
            {
                return Fail(Main.code400, "This trading account is already linked to your profile.");
            }

            entity.TradingAcc = tradingAcc;
            if (Guid.TryParse(request.BrokerId, out var brokerId))
            {
                entity.BrokerId = brokerId;
            }
            entity.UpdatedBy = userId;
            entity.UpdatedAt = DateTime.UtcNow;

            var updated = await _userTradingAccountRepo.UpdateAsync(entity);
            return new ResponseDto<UserTradingAccount>
            {
                responseCode = Main.code200,
                responseMessage = "Trading account updated successfully.",
                data = updated
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while updating the trading account: {ex.Message}");
        }
    }

    public async Task<ResponseDto<UserTradingAccount>> DeleteTradingAccount(string id, string userId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Fail(Main.code400, "Your session has expired. Please sign in again.");
            }

            if (!Guid.TryParse(id, out var accountId))
            {
                return Fail(Main.code400, "Invalid trading account.");
            }

            var entity = await _userTradingAccountRepo.GetEntityAsync(accountId, userId);
            if (entity is null)
            {
                return Fail(Main.code400, "Trading account not found.");
            }

            await _userTradingAccountRepo.DeleteAsync(entity);
            return new ResponseDto<UserTradingAccount>
            {
                responseCode = Main.code200,
                responseMessage = "Trading account removed successfully.",
                data = null
            };
        }
        catch (Exception ex)
        {
            return Fail(Main.code500, $"An error occurred while deleting the trading account: {ex.Message}");
        }
    }

    private static ResponseDto<UserTradingAccount> Fail(int code, string message) => new()
    {
        responseCode = code,
        responseMessage = message,
        data = null
    };
}
