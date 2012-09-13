using System.ComponentModel;
using System.Net.Mail;

namespace Aqueduct.Mail
{
	public class MailMessageSendingEventArgs : CancelEventArgs
	{
		private readonly System.Net.Mail.MailMessage m_mailMessage;
		private readonly SmtpClient m_smtpClient;

		public MailMessageSendingEventArgs(System.Net.Mail.MailMessage mailMessage, SmtpClient smtpClient)
		{
			m_mailMessage = mailMessage;
			m_smtpClient = smtpClient;
		}

		public System.Net.Mail.MailMessage MailMessage
		{
			get { return m_mailMessage; }
		}

		public SmtpClient SmtpClient
		{
			get { return m_smtpClient; }
		}
	}
}