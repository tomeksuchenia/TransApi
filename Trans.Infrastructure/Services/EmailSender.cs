using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Settings;

namespace Trans.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailHostSettings _emailHost;
        public EmailSender(EmailHostSettings emailHost)
        {
            _emailHost = emailHost;
        }
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailHost.EmailUsername));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailHost.EmailHostService, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailHost.EmailUsername, _emailHost.EmailPassword);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
