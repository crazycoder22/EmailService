using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailService.Service.Interface
{
    public interface IEmailBroker
    {
        Task SendEmail(Guid templateId, string emailAddress, string subject, Dictionary<string, string> parameters);
    }
}
