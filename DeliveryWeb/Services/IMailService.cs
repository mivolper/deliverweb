using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string toEmail, string subject, string content);
    }
}
