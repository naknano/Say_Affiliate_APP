namespace WebApplication1.Util;

public static class Validator
{
    // Validate user for new registration or create
    public static Task<bool> ValidateUserAsync(string phoneNumber)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return Task.FromResult(false);
            if (phoneNumber.Length < 9) return Task.FromResult(false);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}