using EmailService.Common.Model;
using EmailService.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EmailService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var emailBroker = serviceProvider.GetService<IEmailBroker>();
            var sqsClientConfig = serviceProvider.GetService<SqsClientConfig>();
            var sqsClient = new SQSClient(sqsClientConfig);

            while (true)
            {
                var response = sqsClient.ReadSQSQueue();
                if (!string.IsNullOrEmpty(response.Item2))
                {
                    JObject o2 = JObject.Parse(response.Item2);
                    var templateId = Guid.Parse(o2["TemplateId"].ToString());
                    var emailAddress = o2["EmailAddress"].ToString();
                    var subject = o2["Subject"].ToString();
                    var parametersDictionary = ParseParameter(o2["Parameter"].ToString());

                    emailBroker.SendEmail(templateId, emailAddress, subject, parametersDictionary).Wait();
                    sqsClient.Acknowledge(response.Item1);
                }

                Thread.Sleep(2000);
            }
        }

        private static Dictionary<string, string> ParseParameter(string parameteJson)
        {
            var parameters = new Dictionary<string, string>();
            dynamic json = JsonConvert.DeserializeObject(parameteJson);
            foreach (var item in json)
            {
                parameters.Add(item.Name.ToString(), item.Value.ToString());
            }

            return parameters;
        }
    }
}
