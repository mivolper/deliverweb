using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Services
{
    public class SendGridMailService : IMailService
    {
        public Task SendMailAsync(string toEmail, string subject, string content)
        {
            throw new NotImplementedException();
        }
    }
}
