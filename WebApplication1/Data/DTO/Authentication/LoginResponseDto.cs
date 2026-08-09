namespace WebApplication1.Data.DTO.Authentication;


public class LoginResponseDto
{
    public string accessToken { get; set; } = string.Empty;
    public string refreshToken { get; set; } = string.Empty;
    public string resetToken { get; set; } = string.Empty;
    public double refreshTokenDays { get; set; }

    
    public string email { get; set; } = string.Empty;
    public string phoneNumber { get; set; } = string.Empty;
    public string userId { get; set; } = string.Empty;
}