using System;

namespace Aqueduct.Mail
{
    public class EmailUser
    {
        private readonly string m_address;
        private readonly string m_name;

        public EmailUser (string name, string address)
        {
            m_name = name;
            m_address = address;
        }

        public EmailUser (string address)
        {
            m_name = address;
            m_address = address;
        }

        public string Name
        {
            get { return m_name; }
        }

        public string Address
        {
            get { return m_address; }
        }

        public static EmailUser Parse (string text)
        {
            string[] arr = text.Split (',');
            if (arr.Length == 1)
                return new EmailUser (text);
            return new EmailUser (arr [0], arr [1]);
        }

        public override string ToString ()
        {
            if (m_name == m_address)
                return m_name;

            return String.Format ("\"{0}\" <{1}> ", m_name, m_address);
        }
    }
}