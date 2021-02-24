using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raccoon.Devkits.EmailToolkit
{
    public class EmailSendService:IDisposable
    {
        private readonly SmtpClient _smtpClient;
        public EmailSendService(string userName,string password,string host,int port=25, SecureSocketOptions secureSocketOptions=SecureSocketOptions.None)
        {
            _smtpClient = new();
            _smtpClient.Connect(host, port, secureSocketOptions);
            _smtpClient.Authenticate(userName, password);
        }

        public async ValueTask SendEmailAsync(IEnumerable<string> fromAddresses, IEnumerable<string> toAddresses,
            string subject, string htmlPayload)
        {
            MimeMessage email = new(
                fromAddresses.Select(MailboxAddress.Parse),
                toAddresses.Select(MailboxAddress.Parse),
                subject,
                new TextPart(TextFormat.Html) { Text = htmlPayload });
            await _smtpClient.SendAsync(email);
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if(!_disposed)
            {
                _smtpClient.Disconnect(true);
                _smtpClient.Dispose();
                _disposed = true;
            }
        }
    }
}
