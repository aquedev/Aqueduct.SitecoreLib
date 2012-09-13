using System;
using System.IO;

namespace Aqueduct.Mail
{
    public class FileAttachment : Attachment
    {
        protected string m_filename = String.Empty;

        public FileAttachment(string filename)
        {
            m_filename = filename;
        }

        public override string FileName
        {
            get { return m_filename; }
        }

        public override bool UsesStream
        {
            get { return false; }
        }

        public override Stream ContentStream
        {
            get { throw new NotImplementedException(); }
        }

    }
}