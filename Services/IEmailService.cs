using System.Threading.Tasks;
namespace E_learning_platform.Services
{
    public interface IEmailService
    {
        Task SendActivationEmailAsync(string toEmail, string token);
    }
}
