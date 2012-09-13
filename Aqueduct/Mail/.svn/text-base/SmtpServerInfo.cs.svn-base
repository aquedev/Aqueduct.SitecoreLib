using System;

namespace Aqueduct.Mail
{
    [Serializable]
    public class SmtpServerInfo 
    {
        private int m_SmtpPort = 25;
        private string m_SmtpUser;

        public SmtpServerInfo ()
        {
        }

        public SmtpServerInfo (string hostName)
        {
            SmtpHost = hostName;
        }

        public string SmtpHost { get; set; }

        public string SmtpUser
        {
            get { return m_SmtpUser; }
            set { m_SmtpUser = value; }
        }

        public string SmtpPassword { get; set; }

        public int SmtpPort
        {
            get { return m_SmtpPort; }
            set { m_SmtpPort = value; }
        }

        public bool UseAuthentication
        {
            get { return !String.IsNullOrEmpty (m_SmtpUser); }
        }

    }
}