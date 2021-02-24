using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raccoon.Devkits.EmailToolkit
{
    public static class EmailSendServiceExtensions
    {
        public static IServiceCollection AddEmailSendService(this IServiceCollection services,string userName, 
            string password, string host, int port = 25, SecureSocketOptions secureSocketOptions = SecureSocketOptions.None)
            => services.AddTransient<EmailSendService>(serviceProvider =>
                new(userName, password, host, port, secureSocketOptions));
        
    }
}
