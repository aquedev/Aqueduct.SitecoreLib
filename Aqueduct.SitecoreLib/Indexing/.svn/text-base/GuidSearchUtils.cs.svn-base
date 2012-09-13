using System;

namespace Aqueduct.SitecoreLib.Indexing
{
    public static class GuidSearchUtils
    {
        public static string ConvertGuidToString(Guid guid)
        {
            // Special characters (such as {, } and - used in guids) must be escaped as they are part of the Lucene.NET query syntax
            // "http://lucene.apache.org/java/2_3_2/queryparsersyntax.html#Escaping Special Characters"
            string[] escapedId = guid.ToString().ToLower().Split(new[] { "{", "}", "-" }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join("", escapedId);
        }
    }
}
