using EmailService.Common.Model;
using EmailService.DataAccess;
using EmailService.EmailClient.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace EmailService.Console
{
    public class Startup
    {
        public Startup()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            var contentRootPath = new FileInfo(location.AbsolutePath).Directory.FullName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(contentRootPath)
                .AddJsonFile($"{contentRootPath}\\appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var smtpSetting = Configuration.GetSection("SmtpSetting").Get<SMTPSetting>();
            var sqsConfigSetting = Configuration.GetSection("SqsClientConfig").Get<SqsClientConfig>();
            services.AddTransient(x => smtpSetting);
            services.AddTransient(x => sqsConfigSetting);
            services.AddTransient<IEmailClient, EmailClient.EmailClient>();
            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ApplicationDb")));
            Repository.Repository.Register(services);
            Service.Service.Register(services);
        }
    }
}
