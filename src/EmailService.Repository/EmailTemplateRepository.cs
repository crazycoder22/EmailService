using EmailService.Common.Model;
using EmailService.DataAccess;
using EmailService.DataAccess.DataModel;
using EmailService.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmailService.Repository
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmailTemplateRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<EmailTemplateDTO> GetEmailTemplate(Guid templateId)
        {
            var emailTempalte = await _applicationDbContext.EmailTemplates.FirstOrDefaultAsync(x => x.Id == templateId);
            if (emailTempalte == default(EmailTemplate))
            {
                throw new Exception("Email template is not registered.");
            }

            return new EmailTemplateDTO
            {
                Id = emailTempalte.Id,
                Template = emailTempalte.Template,
                AppName = emailTempalte.AppName
            };
        }
    }
}
