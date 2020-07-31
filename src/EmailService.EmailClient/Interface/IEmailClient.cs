namespace EmailService.EmailClient.Interface
{
    public interface IEmailClient
    {
        void Send(string from, string to, string subject, string body);
    }
}
