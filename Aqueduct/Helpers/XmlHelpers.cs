using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Aqueduct.Helpers
{
    public static class XmlHelpers
    {
        

        public static bool ValidateXml (string xml)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument ();
                xmlDoc.LoadXml (xml);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static XmlDocument GetBlankXmlDoc ()
        {
            XmlDocument xmlDocument = new XmlDocument ();
            xmlDocument.AppendChild (xmlDocument.CreateXmlDeclaration ("1.0", null, null));
            xmlDocument.AppendChild (xmlDocument.CreateElement ("root"));

            return xmlDocument;
        }

        public static string Transform (string xml, string xslFile)
        {
            return Transform (xml, xslFile, null);
        }

        public static string Transform (string xml, string xslFile, IDictionary<string, string> xslParams)
        {
            XslCompiledTransform xslTrans = new XslCompiledTransform ();
            xslTrans.Load (xslFile);

            return Transform (xml, xslTrans, xslParams);
        }

        public static string Transform (string xml, XslCompiledTransform xslTrans)
        {
            return Transform (xml, xslTrans, null);
        }

        public static string Transform (string xslFile, IDictionary<string, string> xslParams)
        {
            string xml = GetBlankXmlDoc ().OuterXml;

            XslCompiledTransform xslTrans = new XslCompiledTransform ();
            xslTrans.Load (xslFile);

            return Transform (xml, xslTrans, xslParams);
        }

        public static string Transform (XslCompiledTransform xslTrans, IDictionary<string, string> xslParams)
        {
            return Transform (GetBlankXmlDoc ().InnerXml, xslTrans, xslParams);
        }

        public static string Transform (string xml, XslCompiledTransform xslTrans, IDictionary<string, string> xslParams)
        {
            if (xml.Trim ().Length == 0)
                return String.Empty;

            XmlDocument xmlDoc = new XmlDocument ();
            xmlDoc.LoadXml (xml);

            XsltArgumentList xslArgs = null;

            if (xslParams != null)
            {
                xslArgs = new XsltArgumentList ();

                foreach (string paramName in xslParams.Keys)
                {
                    xslArgs.AddParam (paramName, "", xslParams [paramName]);
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                xslTrans.Transform(xmlDoc, xslArgs, sw);
                return sw.ToString();
            }
        }
    }
}