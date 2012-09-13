using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Aqueduct.Mail
{
	public abstract class MailMessage
	{
		private readonly List<Attachment> m_attachments;
		private readonly List<EmailUser> m_bccList;
		private readonly List<EmailUser> m_ccList;
		private readonly bool m_isHtml;
		private readonly List<EmailUser> m_recipients;
		private string m_body = String.Empty;
		private EmailUser m_sender;
		private string m_subject = String.Empty;

		protected MailMessage(bool isHtml)
			: this()
		{
			m_isHtml = isHtml;
		}

		protected MailMessage()
		{
			m_recipients = new List<EmailUser>();
			m_ccList = new List<EmailUser>();
			m_bccList = new List<EmailUser>();
			m_attachments = new List<Attachment>();
		}

		public string Subject
		{
			get { return m_subject; }
			set { m_subject = value; }
		}

		public string Body
		{
			get { return m_body; }
			set { m_body = value; }
		}

		public string RecipientsAsString
		{
			get { return getUserList(m_recipients); }
		}

		public string CcListAsString
		{
			get { return getUserList(m_ccList); }
		}

		public string BccListAsString
		{
			get { return getUserList(m_bccList); }
		}

		protected string SenderAsString
		{
			get { return m_sender.ToString(); }
		}

		protected List<Attachment> Attachments
		{
			get { return m_attachments; }
		}

		public EmailUser Sender
		{
			set { m_sender = value; }
			get { return m_sender; }
		}

		public void AddAttachment(Attachment attachment)
		{
			m_attachments.Add(attachment);
		}

		public void ClearAttachments()
		{
			m_attachments.Clear();
		}

		public virtual void Send(SmtpServerInfo serverInfo)
		{
			System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage {IsBodyHtml = m_isHtml};

			// message.To.Add(string addresses) overload does not encode non ASCII chars
			// http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=338631
			// so have switched to using message.To.Add(MailAddress) overload instead
			foreach (EmailUser user in m_recipients)
			{
				message.To.Add(new MailAddress(user.Address, user.Name));
			}

			foreach (EmailUser user in m_ccList)
			{
				message.CC.Add(new MailAddress(user.Address, user.Name));
			}

			foreach (EmailUser user in m_bccList)
			{
				message.Bcc.Add(new MailAddress(user.Address, user.Name));
			}

			message.Subject = m_subject;
			message.Body = m_body;
			message.From = new MailAddress(SenderAsString);

			foreach (Attachment attachment in m_attachments)
			{
				message.Attachments.Add(ConvertAttachment(attachment));
			}

			SmtpClient client = new SmtpClient(serverInfo.SmtpHost, serverInfo.SmtpPort);
			if (serverInfo.UseAuthentication)
			{
				client.Credentials = null;
			}

			MailMessageSendingEventArgs mailMessageSendingEventArgs = new MailMessageSendingEventArgs(message, client);
			OnSending(mailMessageSendingEventArgs);

			if (!mailMessageSendingEventArgs.Cancel)
			{
				// action was not cancelled
				client.Send(message);

				MailMessageSentEventArgs mailMessageSentEventArgs = new MailMessageSentEventArgs(message);
				OnSent(mailMessageSentEventArgs);
			}
		}

		///<summary>
		/// Occurs before the <see cref="MailMessage"/> is sent. Cancellable.
		///</summary>
		public event EventHandler<MailMessageSendingEventArgs> Sending;

		///<summary>
		/// Occurs after the <see cref="MailMessage"/> is sent.
		///</summary>
		public event EventHandler<MailMessageSentEventArgs> Sent;

		protected void OnSending(MailMessageSendingEventArgs e)
		{
			EventHandler<MailMessageSendingEventArgs> temp = Sending;

			if (temp != null)
				temp(this, e);
		}

		protected void OnSent(MailMessageSentEventArgs e)
		{
			EventHandler<MailMessageSentEventArgs> temp = Sent;

			if (temp != null)
				temp(this, e);
		}

		private static System.Net.Mail.Attachment ConvertAttachment(Attachment attachment)
		{
			if (attachment.UsesStream)
			{
				return new System.Net.Mail.Attachment(attachment.ContentStream, attachment.FileName);
			}
			return new System.Net.Mail.Attachment(attachment.FileName);
		}

		public void ClearAllRecipients()
		{
			ClearRecipients();
			ClearCCList();
			ClearBCCList();
		}

		public void ClearRecipients()
		{
			m_recipients.Clear();
		}

		public void ClearCCList()
		{
			m_ccList.Clear();
		}

		public void ClearBCCList()
		{
			m_bccList.Clear();
		}

		public void AddRecipient(EmailUser user)
		{
			addToList(m_recipients, user);
		}

		public void AddCC(EmailUser user)
		{
			addToList(m_ccList, user);
		}

		public void AddBCC(EmailUser user)
		{
			addToList(m_bccList, user);
		}

		private static void addToList(ICollection<EmailUser> list, EmailUser user)
		{
			if (emailOK(user.Address))
				list.Add(user);
		}

		private static bool emailOK(string email)
		{
			return email != null && email.LastIndexOf(".") > email.LastIndexOf("@");
		}

		private static string getUserList(IEnumerable<EmailUser> userList)
		{
			string result = "";
			int index = 0;

			foreach (EmailUser user in userList)
			{
				if (++index > 1)
					result += ", ";
				result += user.ToString();
			}

			return result;
		}
	}
}