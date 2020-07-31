using EmailService.EmailClient.Interface;
using EmailService.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailService.Service.Test
{
    [TestClass]
    public class EmailBrokerTest
    {
        [TestMethod]
        public void SendEmail_Success()
        {
            // Arrange
            var emailTemplateRepoMock = new Mock<IEmailTemplateRepository>();
            var emailClientMock = new Mock<IEmailClient>();
            var emailTemplateRepoObj = emailTemplateRepoMock.Object;
            var emailClientObj = emailClientMock.Object;
            var templateId = Guid.NewGuid();
            Dictionary<string, string> parameter = new Dictionary<string, string> {
                { "EmployeeName", "LAK123" },
                { "From", "2020-01-01" },
                { "To", "2020-10-01" },
                { "RequestId", "10DE3EBC-3DA6-44B4-C950-08D833C6A26D" }
            };

            var template = "Hello,<br /><br />  Following leave request has been submitted for your approval.<br />  EmployeeName: $$EmployeeName<br />  From: $$From  To: $$To  <br /><br />  Thanks,<br />  LMS    <p>*******************************************************************<br />  *******************$$$$RequestId$$**********************************<br />  ********************Do Not Update this content*************************<br />  </p>  ";
            emailTemplateRepoMock.Setup(x => x.GetEmailTemplate(templateId)).Returns(Task.FromResult(new Common.Model.EmailTemplateDTO
            {
                Id = templateId,
                AppName = "LMS",
                Template = template
            }));

            var emailBroker = new EmailBroker(emailTemplateRepoObj, emailClientObj);

            // Act 
            emailBroker.SendEmail(templateId, "abc@gmail.com", "Leave Approval", parameter).Wait();

            // Assert
            emailClientMock.Verify(x => x.Send("EmailService@gmail.com", "abc@gmail.com", "Leave Approval", "Hello,<br /><br />  Following leave request has been submitted for your approval.<br />  EmployeeName: $LAK123<br />  From: $2020-01-01  To: $2020-10-01  <br /><br />  Thanks,<br />  LMS    <p>*******************************************************************<br />  *******************$$$10DE3EBC-3DA6-44B4-C950-08D833C6A26D$$**********************************<br />  ********************Do Not Update this content*************************<br />  </p>  "));

        }
    }
}
