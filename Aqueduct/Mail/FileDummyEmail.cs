using System.IO;

namespace Aqueduct.Mail
{
    public class FileDummyEmail : MailMessage
    {
        private readonly string m_outputFile;

        public FileDummyEmail (string outputFile)
        {
            m_outputFile = outputFile;
        }

        public override void Send (SmtpServerInfo serverInfo)
        {
            using (TextWriter writer = File.CreateText (m_outputFile))
            {
                writer.WriteLine (Body);
            }
        }
    }
}