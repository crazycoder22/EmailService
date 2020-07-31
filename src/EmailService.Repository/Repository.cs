using EmailService.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Repository
{
    public class Repository
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        }
    }
}
