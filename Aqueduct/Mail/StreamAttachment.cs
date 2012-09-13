using System.IO;

namespace Aqueduct.Mail
{
    public class StreamAttachment : Attachment
    {
        private readonly Stream m_contentStream;
        protected readonly string m_filename;

        protected StreamAttachment(string filename, Stream contentStream)
        {
            m_filename = filename;
            m_contentStream = contentStream;
        }

        public override string FileName
        {
            get { return m_filename; }
        }

        public override bool UsesStream
        {
            get { return true; }
        }

        public override Stream ContentStream
        {
            get { return m_contentStream; }
        }
    }
}