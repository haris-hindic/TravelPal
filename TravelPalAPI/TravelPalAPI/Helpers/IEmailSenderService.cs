using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Helpers
{
    public interface IEmailSenderService
    {
        public void SendEmail(IConfiguration configuration, string receiverName, string receiverEmail, string subject, string message);
    }
}
