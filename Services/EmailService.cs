using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_learning_platform.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendActivationEmailAsync(string toEmail, string token)
        {
            var smtp = _configuration.GetSection("Smtp");
            var host = smtp["Host"];
            var port = int.TryParse(smtp["Port"], out var p) ? p : 0;
            var user = smtp["Username"];
            var pass = smtp["Password"];
            var from = smtp["From"];
            var ssl = bool.TryParse(smtp["EnableSsl"], out var e) ? e : true;
            var appBaseUrl = _configuration["App:BaseUrl"] ?? "http://localhost:5000";
            var link = $"{appBaseUrl}/api/auth/verify-email?email={WebUtility.UrlEncode(toEmail)}&token={WebUtility.UrlEncode(token)}";

            if (string.IsNullOrWhiteSpace(host) || port == 0 || string.IsNullOrWhiteSpace(from))
            {
                System.Console.WriteLine($"Activate account: {link}");
                return;
            }

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = ssl,
                Credentials = new NetworkCredential(user, pass)
            };
            using var message = new MailMessage(from, toEmail)
            {
                Subject = "Kích hoạt tài khoản",
                Body = $"Vui lòng nhấn vào liên kết sau để kích hoạt tài khoản: {link}"
            };
            await client.SendMailAsync(message);
        }
    }
}
