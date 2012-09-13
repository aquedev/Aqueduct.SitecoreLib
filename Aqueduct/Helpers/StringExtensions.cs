using System;

namespace Aqueduct.Helpers
{
    /// <summary>
    /// String Extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Check if a string IS null or empty
        /// </summary>
        /// <param name="str">string to test</param>
        /// 
        [Obsolete("Use the extensions in Aqueduct.Extensions instead")]
        public static bool IsNullOrEmpty (this string str)
        {
            return string.IsNullOrEmpty (str);
        }

        /// <summary>
        /// Checks whether a string is NOT null or empty
        /// </summary>
        /// <param name="str">string to test</param>
        /// 
        [Obsolete("Use the extensions in Aqueduct.Extensions instead")]
        public static bool IsNotEmpty (this string str)
        {
            return !str.IsNullOrEmpty ();
        }


        /// <summary>
        /// Works the same way as String.Format
        /// </summary>
        /// <param name="str">the string to be formatted</param>
        /// <param name="args">arguments that will be inserted in the string</param>
        /// <returns>formatted string</returns>
        /// <example>"{0} is {1} sample".Format("This", 1);</example>
        /// 
        [Obsolete("Use the extensions in Aqueduct.Extensions instead")]
        public static string Format (this string str, params object[] args)
        {
            return string.Format (str, args);
        }

        [Obsolete("Use the extensions in Aqueduct.Extensions instead")]
        public static bool EqualsIgnoreCase(this string str, string value)
        {
            return str.Equals(value, System.StringComparison.CurrentCultureIgnoreCase);
        }
    }
}