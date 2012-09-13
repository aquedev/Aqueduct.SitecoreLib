using System;

namespace Aqueduct.Mail
{
	public class MailMessageSentEventArgs : EventArgs
	{
		private readonly System.Net.Mail.MailMessage m_mailMessage;

		public MailMessageSentEventArgs(System.Net.Mail.MailMessage mailMessage)
		{
			m_mailMessage = mailMessage;
		}

		public System.Net.Mail.MailMessage MailMessage
		{
			get { return m_mailMessage; }
		}
	}
}