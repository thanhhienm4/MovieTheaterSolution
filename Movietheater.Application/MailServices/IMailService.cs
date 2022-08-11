﻿namespace MovieTheater.Application.MailServices
{
    using MovieTheater.Models.Utilities;
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}