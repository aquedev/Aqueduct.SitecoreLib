
namespace Aqueduct.Mail
{
    public class LoggingMailMessage : MailMessage
    {
        private readonly LogMessage m_logMessage;

        public LoggingMailMessage(LogMessage logMessage)
        {
            m_logMessage = logMessage;
        }

        public override void Send(SmtpServerInfo serverInfo)
        {
            m_logMessage("Sender - {0}", SenderAsString);
            m_logMessage("Recipients - {0}", RecipientsAsString);
            m_logMessage("Cc - {0}", CcListAsString);
            m_logMessage("Bcc - {0}", BccListAsString);

            m_logMessage("Subject - {0}", Subject);
            m_logMessage("Body - {0}", Body);
            m_logMessage("Smtp Server - {0}", serverInfo.SmtpHost);

            foreach (Attachment attachment in Attachments)
            {
                m_logMessage("Attachment - {0}", attachment.FileName);
            }

            m_logMessage("");
            m_logMessage("--------------------------------------------------------");
        }
    }

    public delegate void LogMessage(string format, params object[] args);

}