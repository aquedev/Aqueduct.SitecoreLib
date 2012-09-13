using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Aqueduct.Diagnostics;

namespace Aqueduct.Utils
{
    public abstract class CSVService
    {
        protected readonly ILogger m_logger = AppLogger.GetNamedLogger(typeof(CSVService));

        public abstract void WriteLine(IDictionary<string, string> keyValues);
        public abstract void WriteLine(IDictionary<string, string> keyValues, bool writeHeader);

        protected static string[] FromCollection(ICollection<string> values)
        {
            if (values == null || values.Count == 0)
                return new string[] { };

            string[] retVals = new string[values.Count];
            int count = 0;
            foreach (string str in values)
                retVals[count++] = str;
            return retVals;
        }

        protected static string CreateCSVLine(ICollection<string> values)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string value in values)
            {
                string processedValue = ProcessMultilineContent(value);
                sb.AppendFormat("\"{0}\",", processedValue.Replace("\"", "\"\""));
            }
            if (sb.Length > 0)
                sb = sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        protected static string ProcessMultilineContent(string input)
        {
            if (string.IsNullOrEmpty(input) || !input.Contains(Environment.NewLine))
                return input;
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(", ", lines);
        }

        protected static void WriteLineInternal(TextWriter writer, IDictionary<string, string> keyValues, bool writeHeader)
        {
            if (writeHeader)
                writer.WriteLine(CreateCSVLine(keyValues.Keys));
            writer.WriteLine(CreateCSVLine(keyValues.Values));
        }
    }
}
