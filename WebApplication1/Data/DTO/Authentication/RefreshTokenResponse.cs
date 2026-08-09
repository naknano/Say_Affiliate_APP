namespace WebApplication1.Data.DTO.Authentication;

public class RefreshTokenResponse
{
    public string refreshToken { get; set; } = string.Empty;
    public string accessToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
}