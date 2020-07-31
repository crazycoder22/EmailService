using EmailService.Common.Model;
using System;
using System.Threading.Tasks;

namespace EmailService.Repository.Interface
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplateDTO> GetEmailTemplate(Guid templateId);
    }
}
