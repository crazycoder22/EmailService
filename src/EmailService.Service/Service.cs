using EmailService.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Service
{
    public class Service
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IEmailBroker, EmailBroker>();
        }
    }
}
