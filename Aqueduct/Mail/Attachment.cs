using System.IO;

namespace Aqueduct.Mail
{
    public abstract class Attachment
    {
        public abstract bool UsesStream { get; }
        public abstract string FileName { get; }
        public abstract Stream ContentStream { get; }
    }
}