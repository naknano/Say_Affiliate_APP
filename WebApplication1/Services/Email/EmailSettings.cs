namespace WebApplication1.Services.Email;

public class EmailSettings
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public string SenderName { get; set; } = "SAGPay System";
    public string SenderEmail { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
}
