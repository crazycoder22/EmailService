using EmailService.DataAccess;
using EmailService.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmailService.Repository.Test
{
    [TestClass]
    public class EmailTemplateRepositoryTest
    {
        [TestMethod]
        public void GetEmailTemplate_Success()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("EmailService1");
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            var templateId = Guid.NewGuid();
            applicationDbContext.EmailTemplates.Add(new EmailTemplate
            {
                Id = templateId,
                AppName = "LMS",
                Template = "EmailTemplate"
            });

            applicationDbContext.SaveChanges();
            var emailTemplateRepository = new EmailTemplateRepository(applicationDbContext);

            // Act
            var emailTemplate = emailTemplateRepository.GetEmailTemplate(templateId).Result;

            // Assert
            Assert.AreEqual(emailTemplate.Template, "EmailTemplate");
            Assert.AreEqual(emailTemplate.AppName, "LMS");
        }
    }
}
