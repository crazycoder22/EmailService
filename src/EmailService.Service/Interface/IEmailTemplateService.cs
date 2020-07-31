using EmailService.Common.Model;
using System;
using System.Threading.Tasks;

namespace EmailService.Service.Interface
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplateDTO> GetEmailTemplate(Guid templateId);
    }
}
