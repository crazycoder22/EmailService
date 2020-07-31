using EmailService.Common.Model;
using EmailService.Repository.Interface;
using EmailService.Service.Interface;
using System;
using System.Threading.Tasks;

namespace EmailService.Service
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public Task<EmailTemplateDTO> GetEmailTemplate(Guid templateId)
        {
            return _emailTemplateRepository.GetEmailTemplate(templateId);
        }
    }
}
