using EmailService.Common.Model;
using EmailService.EmailClient.Interface;
using System.Net;
using System.Net.Mail;

namespace EmailService.EmailClient
{
    public class EmailClient : IEmailClient
    {
        private SMTPSetting _smtpSetting;
        public EmailClient(SMTPSetting smtpSetting)
        {
            _smtpSetting = smtpSetting;
        }
        public void Send(string from, string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(from);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            smtp.Port = _smtpSetting.Port;
            smtp.Host = _smtpSetting.Server; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_smtpSetting.UserName, _smtpSetting.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
