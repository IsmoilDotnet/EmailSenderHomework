using EmailSender.Infrastructure.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
namespace EmailSender.Application.Services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(EmailModel model)
        {
            var emailSettings = _config.GetSection("EmailSettings");

            string path = @"C:\Users\dotnetbillioner\Desktop\EmailSender\Homework\EmailSenderHomework\EmailSender.Application\RandomCode.html";

            using (var stream = new StreamReader(path))
            {
                model.Body = await stream.ReadToEndAsync();
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
                Subject = model.Subject,
                Body = model.Body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(model.Email);

            using var smtpClient = new SmtpClient(emailSettings["EmailServer"], int.Parse(emailSettings["MailPort"]))
            {
                Port = Convert.ToInt32(emailSettings["MailPort"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task<bool> CheckEmailAsync(string code)
        {
            return code == "HI";
        }

        public async Task SetPassword(UserModel user)
        {
            string filePath = @"C:\Users\dotnetbillioner\Desktop\EmailSender\Homework\EmailSenderHomework\EmailSender.Application\RegisteredUsers.txt";

            string userEntry = $"{user.Email},{user.Password}\n";

            string[] userEntries = await File.ReadAllLinesAsync(filePath);

            foreach (var entry in userEntries)
            {
                var parts = entry.Split(',');

                if (parts.Length == 2 && parts[0] == user.Email)
                {
                    return;
                }
            }
            await File.AppendAllTextAsync(filePath, userEntry);
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            string filePath = @"C:\Users\dotnetbillioner\Desktop\EmailSender\Homework\EmailSenderHomework\EmailSender.Application\RegisteredUsers.txt";

            string[] userEntries = await File.ReadAllLinesAsync(filePath);

            foreach (var entry in userEntries)
            {
                var parts = entry.Split(',');

                if (parts.Length == 2 && parts[0] == email)
                {
                    return true;
                }
            }

            return false;
        }



        public async Task<bool> VerifyCredentials(UserModel user)
        {
            string filePath = @"C:\Users\dotnetbillioner\Desktop\EmailSender\Homework\EmailSenderHomework\EmailSender.Application\RegisteredUsers.txt";

            string[] userEntries = await File.ReadAllLinesAsync(filePath);

            foreach (var entry in userEntries)
            {
                var parts = entry.Split(',');

                if (parts.Length == 2 && parts[0] == user.Email && parts[1] == user.Password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
