using System;
using System.Collections.Generic;
using System.IO;

namespace Aqueduct.Utils
{
    public class FileCSVService : CSVService
    {
        private object m_lock = new object();
        private string m_filename;

        public FileCSVService(string filename)
        {
            m_filename = filename;
        }

        public override void WriteLine(IDictionary<string, string> keyValues)
        {
            bool firstTime = !File.Exists(m_filename);
            WriteLine(keyValues, firstTime);
        }

        public override void WriteLine(IDictionary<string, string> keyValues, bool writeHeader)
        {
            if (keyValues == null || keyValues.Count == 0)
                return;

            lock (m_lock)
            {
                try
                {
                    using (FileStream fs = new FileStream(m_filename, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sWriter = new StreamWriter(fs))
                        {
                            WriteLineInternal(sWriter, keyValues, writeHeader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    m_logger.LogError(string.Format("Error occured while trying to write to {0}. Values = {1}",
                        m_filename, string.Join(",", FromCollection(keyValues.Values))), ex);
                }
            }
        }
    }
}
