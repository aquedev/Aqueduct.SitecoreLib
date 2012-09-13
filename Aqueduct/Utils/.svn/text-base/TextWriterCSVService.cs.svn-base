using System;
using System.Collections.Generic;
using System.IO;

namespace Aqueduct.Utils
{
    public class TextWriterCSVService : CSVService
    {
        private TextWriter m_writer;
        object m_lock = new object();

        public TextWriterCSVService(TextWriter writer)
        {
            m_writer = writer;
        }

        public override void WriteLine(IDictionary<string, string> keyValues)
        {
            WriteLine(keyValues, false);
        }

        public override void WriteLine(IDictionary<string, string> keyValues, bool writeHeader)
        {
            if (keyValues == null || keyValues.Count == 0)
                return;

            lock (m_lock)
            {
                try
                {
                    WriteLineInternal(m_writer, keyValues, writeHeader);
                }
                catch (Exception ex)
                {
                    m_logger.LogError(string.Format("Error occured while trying to write to TextWriter. Values = {0}"
                        , string.Join(",", FromCollection(keyValues.Values))), ex);
                }
            }
        }
    }
}
