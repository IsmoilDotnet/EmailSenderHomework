using EmailSender.Infrastructure.Entities.Models;

namespace EmailSender.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel model);
        Task<bool> CheckEmailAsync(string code);
        Task SetPassword(UserModel user);
        Task<bool> IsUserRegistered(string email);
        Task<bool> VerifyCredentials(UserModel user);
    }
}
