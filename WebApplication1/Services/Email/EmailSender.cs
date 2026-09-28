using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace WebApplication1.Services.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _settings;

    public EmailSender(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

        using var client = new SmtpClient();

        // Some environments (notably macOS dev machines) can't complete the CRL/OCSP
        // revocation lookup, which otherwise aborts the TLS handshake with Brevo.
        client.CheckCertificateRevocation = false;

        // Port 587 uses STARTTLS (upgrade the plain connection to TLS after greeting).
        await client.ConnectAsync(_settings.Server, _settings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.Username, _settings.Number);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
