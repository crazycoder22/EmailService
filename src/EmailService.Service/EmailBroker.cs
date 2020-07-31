using EmailService.EmailClient.Interface;
using EmailService.Repository.Interface;
using EmailService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Service
{
    public class EmailBroker : IEmailBroker
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailClient _emailClient;
        private const string FROM_EMAIL = "EmailService@gmail.com";

        public EmailBroker(IEmailTemplateRepository emailTemplateRepository, IEmailClient emailClient)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailClient = emailClient;
        }

        public async Task SendEmail(Guid templateId, string emailAddress, string subject, Dictionary<string, string> parameters)
        {
            var emailTemplateFromDb = await _emailTemplateRepository.GetEmailTemplate(templateId);
            var emailBodytemplate = emailTemplateFromDb.Template;
            var emailBody = new StringBuilder(emailBodytemplate);
            foreach (var parameter in parameters)
            {
                var key = parameter.Key;
                emailBody.Replace($"${key}", parameter.Value);
            }

            emailBody.Replace($"$EmailTo", FROM_EMAIL);

            _emailClient.Send(FROM_EMAIL, emailAddress, subject, emailBody.ToString());
        }
    }
}
