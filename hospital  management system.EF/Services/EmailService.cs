using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;



namespace hospital__management_system.EF.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
       
        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            

            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:SenderName"]));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["EmailSettings:SmtpUsername"], _config["EmailSettings:SmtpPassword"]);
            smtp.Send(email);
            smtp.Disconnect(true);
           
        }
    }
}
