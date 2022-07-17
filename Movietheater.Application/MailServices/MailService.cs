using System;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MovieTheater.Models.Utilities;

namespace MovieTheater.Application.MailServices
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        private readonly ILogger<MailService> _logger;


        public MailService(IOptions<MailSettings> _mailSettings, ILogger<MailService> _logger)
        {
            this._mailSettings = _mailSettings.Value;
            this._logger = _logger;
            this._logger.LogInformation("Create SendMailService");
        }

        // Gửi email, theo nội dung trong mailContent
        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);

                _logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                _logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            _logger.LogInformation("send mail to " + mailContent.To);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendMail(new MailContent()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            });
        }
    }
}