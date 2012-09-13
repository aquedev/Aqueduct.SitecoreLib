namespace Aqueduct.Mail
{
    public interface IMailService
    {
        void SendMail(MailMessage message);
    }
}
