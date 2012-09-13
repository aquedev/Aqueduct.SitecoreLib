//   SubSonic - http://subsonicproject.com
// 
//   The contents of this file are subject to the New BSD
//   License (the "License"); you may not use this file
//   except in compliance with the License. You may obtain a copy of
//   the License at http://www.opensource.org/licenses/bsd-license.php
//  
//   Software distributed under the License is distributed on an 
//   "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//   implied. See the License for the specific language governing
//   rights and limitations under the License.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Aqueduct.Extensions
{
    public static class Strings
    {
        private static readonly Dictionary<int, string> m_entityTable = new Dictionary<int, string>();
        private static readonly Dictionary<string, string> m_usStateTable = new Dictionary<string, string>();

        /// <summary>
        /// Initializes the <see cref="Strings"/> class.
        /// </summary>
        static Strings()
        {
            FillEntities();
        }

        public static bool Matches(this string source, string compare)
        {
            return String.Equals(source, compare, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool MatchesTrimmed(this string source, string compare)
        {
            return String.Equals(source.Trim(), compare.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool MatchesRegex(this string inputString, string matchPattern)
        {
            return Regex.IsMatch(inputString, matchPattern,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromEnd">The remove from end.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, int removeFromEnd)
        {
            string result = sourceString;
            if ((removeFromEnd > 0) && (sourceString.Length > removeFromEnd - 1))
                result = result.Remove(sourceString.Length - removeFromEnd, removeFromEnd);
            return result;
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="backDownTo">The back down to.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString, string backDownTo)
        {
            int removeDownTo = sourceString.LastIndexOf(backDownTo);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
                removeFromEnd = sourceString.Length - removeDownTo;

            string result = sourceString;

            if (sourceString.Length > removeFromEnd - 1)
                result = result.Remove(removeDownTo, removeFromEnd);

            return result;
        }

        /// <summary>
        /// Plurals to singular.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string PluralToSingular(this string sourceString)
        {
            return sourceString.MakeSingular();
        }

        /// <summary>
        /// Singulars to plural.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string SingularToPlural(this string sourceString)
        {
            return sourceString.MakePlural();
        }

        /// <summary>
        /// Make plural when count is not one
        /// </summary>
        /// <param name="number">The number of things</param>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Pluralize(this int number, string sourceString)
        {
            if (number == 1)
                return String.Concat(number, " ", sourceString.MakeSingular());
            return String.Concat(number, " ", sourceString.MakePlural());
        }

        /// <summary>
        /// Removes the specified chars from the beginning of a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromBeginning">The remove from beginning.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString, int removeFromBeginning)
        {
            string result = sourceString;
            if (sourceString.Length > removeFromBeginning)
                result = result.Remove(0, removeFromBeginning);
            return result;
        }

        /// <summary>
        /// Removes chars from the beginning of a string, up to the specified string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeUpTo">The remove up to.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString, string removeUpTo)
        {
            int removeFromBeginning = sourceString.IndexOf(removeUpTo);
            string result = sourceString;

            if (sourceString.Length > removeFromBeginning && removeFromBeginning > 0)
                result = result.Remove(0, removeFromBeginning);

            return result;
        }

        /// <summary>
        /// Strips the last char from a a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Chop(this string sourceString)
        {
            return Chop(sourceString, 1);
        }

        /// <summary>
        /// Strips the last char from a a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Clip(this string sourceString)
        {
            return Clip(sourceString, 1);
        }

        /// <summary>
        /// Fasts the replace.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns></returns>
        public static string FastReplace(this string original, string pattern, string replacement)
        {
            return FastReplace(original, pattern, replacement, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Fasts the replace.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns></returns>
        public static string FastReplace(this string original, string pattern, string replacement,
                                         StringComparison comparisonType)
        {
            if (original == null)
                return null;

            if (String.IsNullOrEmpty(pattern))
                return original;

            int lenPattern = pattern.Length;
            int idxPattern = -1;
            int idxLast = 0;

            StringBuilder result = new StringBuilder();

            while (true)
            {
                idxPattern = original.IndexOf(pattern, idxPattern + 1, comparisonType);

                if (idxPattern < 0)
                {
                    result.Append(original, idxLast, original.Length - idxLast);
                    break;
                }

                result.Append(original, idxLast, idxPattern - idxLast);
                result.Append(replacement);

                idxLast = idxPattern + lenPattern;
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns text that is located between the startText and endText tags.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="startText">The text from which to start the crop</param>
        /// <param name="endText">The endpoint of the crop</param>
        /// <returns></returns>
        public static string Crop(this string sourceString, string startText, string endText)
        {
            int startIndex = sourceString.IndexOf(startText, StringComparison.CurrentCultureIgnoreCase);
            if (startIndex == -1)
                return String.Empty;

            startIndex += startText.Length;
            int endIndex = sourceString.IndexOf(endText, startIndex, StringComparison.CurrentCultureIgnoreCase);
            if (endIndex == -1)
                return String.Empty;

            return sourceString.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Removes excess white space in a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Squeeze(this string sourceString)
        {
            char[] delim = { ' ' };
            string[] lines = sourceString.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string s in lines)
            {
                if (!String.IsNullOrEmpty(s.Trim()))
                    sb.Append(s + " ");
            }
            //remove the last pipe
            string result = Chop(sb.ToString());
            return result.Trim();
        }

        /// <summary>
        /// Removes all non-alpha numeric characters in a string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string ToAlphaNumericOnly(this string sourceString)
        {
            return Regex.Replace(sourceString, @"\W*", "");
        }

        /// <summary>
        /// Creates a string array based on the words in a sentence
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string[] ToWords(this string sourceString)
        {
            string result = sourceString.Trim();
            return result.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Strips all HTML tags from a string
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <returns></returns>
        public static string StripHTML(this string htmlString)
        {
            return StripHTML(htmlString, String.Empty);
        }

        /// <summary>
        /// Strips all HTML tags from a string and replaces the tags with the specified replacement
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <param name="htmlPlaceHolder">The HTML place holder.</param>
        /// <returns></returns>
        public static string StripHTML(this string htmlString, string htmlPlaceHolder)
        {
            const string pattern = @"<(.|\n)*?>";
            string sOut = Regex.Replace(htmlString, pattern, htmlPlaceHolder);
            sOut = sOut.Replace("&nbsp;", String.Empty);
            sOut = sOut.Replace("&amp;", "&");
            sOut = sOut.Replace("&gt;", ">");
            sOut = sOut.Replace("&lt;", "<");
            return sOut;
        }

        public static List<string> FindMatches(this string source, string find)
        {
            Regex reg = new Regex(find, RegexOptions.IgnoreCase);

            List<string> result = new List<string>();
            foreach (Match m in reg.Matches(source))
                result.Add(m.Value);
            return result;
        }

        /// <summary>
        /// Converts a generic List collection to a single comma-delimitted string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static string ToDelimitedList(this IEnumerable<string> list)
        {
            return ToDelimitedList(list, ",");
        }

        /// <summary>
        /// Converts a generic List collection to a single string using the specified delimitter.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToDelimitedList(this IEnumerable<string> list, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in list)
                sb.Append(String.Concat(s, delimiter));
            string result = sb.ToString();
            result = Chop(result);
            return result;
        }

        /// <summary>
        /// Strips the specified input.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="stripValue">The strip value.</param>
        /// <returns></returns>
        public static string Strip(this string sourceString, string stripValue)
        {
            if (!String.IsNullOrEmpty(stripValue))
            {
                string[] replace = stripValue.Split(new[] { ',' });
                for (int i = 0; i < replace.Length; i++)
                {
                    if (!String.IsNullOrEmpty(sourceString))
                        sourceString = Regex.Replace(sourceString, replace[i], String.Empty);
                }
            }
            return sourceString;
        }

        /// <summary>
        /// Converts ASCII encoding to Unicode
        /// </summary>
        /// <param name="asciiCode">The ASCII code.</param>
        /// <returns></returns>
        public static string AsciiToUnicode(this int asciiCode)
        {
            Encoding ascii = Encoding.UTF32;
            char c = (char)asciiCode;
            Byte[] b = ascii.GetBytes(c.ToString());
            return ascii.GetString((b));
        }

        /// <summary>
        /// Converts Text to HTML-encoded string
        /// </summary>
        /// <param name="textString">The text string.</param>
        /// <returns></returns>
        public static string TextToEntity(this string textString)
        {
            foreach (KeyValuePair<int, string> key in m_entityTable)
                textString = textString.Replace(AsciiToUnicode(key.Key), key.Value);
            return textString.Replace(AsciiToUnicode(38), "&amp;");
        }

        /// <summary>
        /// Converts HTML-encoded bits to Text
        /// </summary>
        /// <param name="entityText">The entity text.</param>
        /// <returns></returns>
        public static string EntityToText(this string entityText)
        {
            entityText = entityText.Replace("&amp;", "&");
            foreach (KeyValuePair<int, string> key in m_entityTable)
                entityText = entityText.Replace(key.Value, AsciiToUnicode(key.Key));
            return entityText;
        }

        /// <summary>
        /// Formats the args using String.Format with the target string as a format string.
        /// </summary>
        /// <param name="fmt">The format string passed to String.Format</param>
        /// <param name="args">The args passed to String.Format</param>
        /// <returns></returns>
        public static string ToFormattedString(this string fmt, params object[] args)
        {
            return String.Format(fmt, args);
        }

        /// <summary>
        /// Strings to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string Value)
        {
            T oOut = default(T);
            Type t = typeof(T);
            foreach (FieldInfo fi in t.GetFields())
            {
                if (fi.Name.Matches(Value))
                    oOut = (T)fi.GetValue(null);
            }

            return oOut;
        }

        /// <summary>
        /// Strings to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">returned if no match is found</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue)
        {
            var result = value.ToEnum<T>();
            return result.Equals(default(T)) 
                ? defaultValue 
                : result;
        }

        /// <summary>
        /// Fills the entities.
        /// </summary>
        private static void FillEntities()
        {
            m_entityTable.Add(160, "&nbsp;");
            m_entityTable.Add(161, "&iexcl;");
            m_entityTable.Add(162, "&cent;");
            m_entityTable.Add(163, "&pound;");
            m_entityTable.Add(164, "&curren;");
            m_entityTable.Add(165, "&yen;");
            m_entityTable.Add(166, "&brvbar;");
            m_entityTable.Add(167, "&sect;");
            m_entityTable.Add(168, "&uml;");
            m_entityTable.Add(169, "&copy;");
            m_entityTable.Add(170, "&ordf;");
            m_entityTable.Add(171, "&laquo;");
            m_entityTable.Add(172, "&not;");
            m_entityTable.Add(173, "&shy;");
            m_entityTable.Add(174, "&reg;");
            m_entityTable.Add(175, "&macr;");
            m_entityTable.Add(176, "&deg;");
            m_entityTable.Add(177, "&plusmn;");
            m_entityTable.Add(178, "&sup2;");
            m_entityTable.Add(179, "&sup3;");
            m_entityTable.Add(180, "&acute;");
            m_entityTable.Add(181, "&micro;");
            m_entityTable.Add(182, "&para;");
            m_entityTable.Add(183, "&middot;");
            m_entityTable.Add(184, "&cedil;");
            m_entityTable.Add(185, "&sup1;");
            m_entityTable.Add(186, "&ordm;");
            m_entityTable.Add(187, "&raquo;");
            m_entityTable.Add(188, "&frac14;");
            m_entityTable.Add(189, "&frac12;");
            m_entityTable.Add(190, "&frac34;");
            m_entityTable.Add(191, "&iquest;");
            m_entityTable.Add(192, "&Agrave;");
            m_entityTable.Add(193, "&Aacute;");
            m_entityTable.Add(194, "&Acirc;");
            m_entityTable.Add(195, "&Atilde;");
            m_entityTable.Add(196, "&Auml;");
            m_entityTable.Add(197, "&Aring;");
            m_entityTable.Add(198, "&AElig;");
            m_entityTable.Add(199, "&Ccedil;");
            m_entityTable.Add(200, "&Egrave;");
            m_entityTable.Add(201, "&Eacute;");
            m_entityTable.Add(202, "&Ecirc;");
            m_entityTable.Add(203, "&Euml;");
            m_entityTable.Add(204, "&Igrave;");
            m_entityTable.Add(205, "&Iacute;");
            m_entityTable.Add(206, "&Icirc;");
            m_entityTable.Add(207, "&Iuml;");
            m_entityTable.Add(208, "&ETH;");
            m_entityTable.Add(209, "&Ntilde;");
            m_entityTable.Add(210, "&Ograve;");
            m_entityTable.Add(211, "&Oacute;");
            m_entityTable.Add(212, "&Ocirc;");
            m_entityTable.Add(213, "&Otilde;");
            m_entityTable.Add(214, "&Ouml;");
            m_entityTable.Add(215, "&times;");
            m_entityTable.Add(216, "&Oslash;");
            m_entityTable.Add(217, "&Ugrave;");
            m_entityTable.Add(218, "&Uacute;");
            m_entityTable.Add(219, "&Ucirc;");
            m_entityTable.Add(220, "&Uuml;");
            m_entityTable.Add(221, "&Yacute;");
            m_entityTable.Add(222, "&THORN;");
            m_entityTable.Add(223, "&szlig;");
            m_entityTable.Add(224, "&agrave;");
            m_entityTable.Add(225, "&aacute;");
            m_entityTable.Add(226, "&acirc;");
            m_entityTable.Add(227, "&atilde;");
            m_entityTable.Add(228, "&auml;");
            m_entityTable.Add(229, "&aring;");
            m_entityTable.Add(230, "&aelig;");
            m_entityTable.Add(231, "&ccedil;");
            m_entityTable.Add(232, "&egrave;");
            m_entityTable.Add(233, "&eacute;");
            m_entityTable.Add(234, "&ecirc;");
            m_entityTable.Add(235, "&euml;");
            m_entityTable.Add(236, "&igrave;");
            m_entityTable.Add(237, "&iacute;");
            m_entityTable.Add(238, "&icirc;");
            m_entityTable.Add(239, "&iuml;");
            m_entityTable.Add(240, "&eth;");
            m_entityTable.Add(241, "&ntilde;");
            m_entityTable.Add(242, "&ograve;");
            m_entityTable.Add(243, "&oacute;");
            m_entityTable.Add(244, "&ocirc;");
            m_entityTable.Add(245, "&otilde;");
            m_entityTable.Add(246, "&ouml;");
            m_entityTable.Add(247, "&divide;");
            m_entityTable.Add(248, "&oslash;");
            m_entityTable.Add(249, "&ugrave;");
            m_entityTable.Add(250, "&uacute;");
            m_entityTable.Add(251, "&ucirc;");
            m_entityTable.Add(252, "&uuml;");
            m_entityTable.Add(253, "&yacute;");
            m_entityTable.Add(254, "&thorn;");
            m_entityTable.Add(255, "&yuml;");
            m_entityTable.Add(402, "&fnof;");
            m_entityTable.Add(913, "&Alpha;");
            m_entityTable.Add(914, "&Beta;");
            m_entityTable.Add(915, "&Gamma;");
            m_entityTable.Add(916, "&Delta;");
            m_entityTable.Add(917, "&Epsilon;");
            m_entityTable.Add(918, "&Zeta;");
            m_entityTable.Add(919, "&Eta;");
            m_entityTable.Add(920, "&Theta;");
            m_entityTable.Add(921, "&Iota;");
            m_entityTable.Add(922, "&Kappa;");
            m_entityTable.Add(923, "&Lambda;");
            m_entityTable.Add(924, "&Mu;");
            m_entityTable.Add(925, "&Nu;");
            m_entityTable.Add(926, "&Xi;");
            m_entityTable.Add(927, "&Omicron;");
            m_entityTable.Add(928, "&Pi;");
            m_entityTable.Add(929, "&Rho;");
            m_entityTable.Add(931, "&Sigma;");
            m_entityTable.Add(932, "&Tau;");
            m_entityTable.Add(933, "&Upsilon;");
            m_entityTable.Add(934, "&Phi;");
            m_entityTable.Add(935, "&Chi;");
            m_entityTable.Add(936, "&Psi;");
            m_entityTable.Add(937, "&Omega;");
            m_entityTable.Add(945, "&alpha;");
            m_entityTable.Add(946, "&beta;");
            m_entityTable.Add(947, "&gamma;");
            m_entityTable.Add(948, "&delta;");
            m_entityTable.Add(949, "&epsilon;");
            m_entityTable.Add(950, "&zeta;");
            m_entityTable.Add(951, "&eta;");
            m_entityTable.Add(952, "&theta;");
            m_entityTable.Add(953, "&iota;");
            m_entityTable.Add(954, "&kappa;");
            m_entityTable.Add(955, "&lambda;");
            m_entityTable.Add(956, "&mu;");
            m_entityTable.Add(957, "&nu;");
            m_entityTable.Add(958, "&xi;");
            m_entityTable.Add(959, "&omicron;");
            m_entityTable.Add(960, "&pi;");
            m_entityTable.Add(961, "&rho;");
            m_entityTable.Add(962, "&sigmaf;");
            m_entityTable.Add(963, "&sigma;");
            m_entityTable.Add(964, "&tau;");
            m_entityTable.Add(965, "&upsilon;");
            m_entityTable.Add(966, "&phi;");
            m_entityTable.Add(967, "&chi;");
            m_entityTable.Add(968, "&psi;");
            m_entityTable.Add(969, "&omega;");
            m_entityTable.Add(977, "&thetasym;");
            m_entityTable.Add(978, "&upsih;");
            m_entityTable.Add(982, "&piv;");
            m_entityTable.Add(8226, "&bull;");
            m_entityTable.Add(8230, "&hellip;");
            m_entityTable.Add(8242, "&prime;");
            m_entityTable.Add(8243, "&Prime;");
            m_entityTable.Add(8254, "&oline;");
            m_entityTable.Add(8260, "&frasl;");
            m_entityTable.Add(8472, "&weierp;");
            m_entityTable.Add(8465, "&image;");
            m_entityTable.Add(8476, "&real;");
            m_entityTable.Add(8482, "&trade;");
            m_entityTable.Add(8501, "&alefsym;");
            m_entityTable.Add(8592, "&larr;");
            m_entityTable.Add(8593, "&uarr;");
            m_entityTable.Add(8594, "&rarr;");
            m_entityTable.Add(8595, "&darr;");
            m_entityTable.Add(8596, "&harr;");
            m_entityTable.Add(8629, "&crarr;");
            m_entityTable.Add(8656, "&lArr;");
            m_entityTable.Add(8657, "&uArr;");
            m_entityTable.Add(8658, "&rArr;");
            m_entityTable.Add(8659, "&dArr;");
            m_entityTable.Add(8660, "&hArr;");
            m_entityTable.Add(8704, "&forall;");
            m_entityTable.Add(8706, "&part;");
            m_entityTable.Add(8707, "&exist;");
            m_entityTable.Add(8709, "&empty;");
            m_entityTable.Add(8711, "&nabla;");
            m_entityTable.Add(8712, "&isin;");
            m_entityTable.Add(8713, "&notin;");
            m_entityTable.Add(8715, "&ni;");
            m_entityTable.Add(8719, "&prod;");
            m_entityTable.Add(8721, "&sum;");
            m_entityTable.Add(8722, "&minus;");
            m_entityTable.Add(8727, "&lowast;");
            m_entityTable.Add(8730, "&radic;");
            m_entityTable.Add(8733, "&prop;");
            m_entityTable.Add(8734, "&infin;");
            m_entityTable.Add(8736, "&ang;");
            m_entityTable.Add(8743, "&and;");
            m_entityTable.Add(8744, "&or;");
            m_entityTable.Add(8745, "&cap;");
            m_entityTable.Add(8746, "&cup;");
            m_entityTable.Add(8747, "&int;");
            m_entityTable.Add(8756, "&there4;");
            m_entityTable.Add(8764, "&sim;");
            m_entityTable.Add(8773, "&cong;");
            m_entityTable.Add(8776, "&asymp;");
            m_entityTable.Add(8800, "&ne;");
            m_entityTable.Add(8801, "&equiv;");
            m_entityTable.Add(8804, "&le;");
            m_entityTable.Add(8805, "&ge;");
            m_entityTable.Add(8834, "&sub;");
            m_entityTable.Add(8835, "&sup;");
            m_entityTable.Add(8836, "&nsub;");
            m_entityTable.Add(8838, "&sube;");
            m_entityTable.Add(8839, "&supe;");
            m_entityTable.Add(8853, "&oplus;");
            m_entityTable.Add(8855, "&otimes;");
            m_entityTable.Add(8869, "&perp;");
            m_entityTable.Add(8901, "&sdot;");
            m_entityTable.Add(8968, "&lceil;");
            m_entityTable.Add(8969, "&rceil;");
            m_entityTable.Add(8970, "&lfloor;");
            m_entityTable.Add(8971, "&rfloor;");
            m_entityTable.Add(9001, "&lang;");
            m_entityTable.Add(9002, "&rang;");
            m_entityTable.Add(9674, "&loz;");
            m_entityTable.Add(9824, "&spades;");
            m_entityTable.Add(9827, "&clubs;");
            m_entityTable.Add(9829, "&hearts;");
            m_entityTable.Add(9830, "&diams;");
            m_entityTable.Add(34, "&quot;");
            //_entityTable.Add(38, "&amp;");
            m_entityTable.Add(60, "&lt;");
            m_entityTable.Add(62, "&gt;");
            m_entityTable.Add(338, "&OElig;");
            m_entityTable.Add(339, "&oelig;");
            m_entityTable.Add(352, "&Scaron;");
            m_entityTable.Add(353, "&scaron;");
            m_entityTable.Add(376, "&Yuml;");
            m_entityTable.Add(710, "&circ;");
            m_entityTable.Add(732, "&tilde;");
            m_entityTable.Add(8194, "&ensp;");
            m_entityTable.Add(8195, "&emsp;");
            m_entityTable.Add(8201, "&thinsp;");
            m_entityTable.Add(8204, "&zwnj;");
            m_entityTable.Add(8205, "&zwj;");
            m_entityTable.Add(8206, "&lrm;");
            m_entityTable.Add(8207, "&rlm;");
            m_entityTable.Add(8211, "&ndash;");
            m_entityTable.Add(8212, "&mdash;");
            m_entityTable.Add(8216, "&lsquo;");
            m_entityTable.Add(8217, "&rsquo;");
            m_entityTable.Add(8218, "&sbquo;");
            m_entityTable.Add(8220, "&ldquo;");
            m_entityTable.Add(8221, "&rdquo;");
            m_entityTable.Add(8222, "&bdquo;");
            m_entityTable.Add(8224, "&dagger;");
            m_entityTable.Add(8225, "&Dagger;");
            m_entityTable.Add(8240, "&permil;");
            m_entityTable.Add(8249, "&lsaquo;");
            m_entityTable.Add(8250, "&rsaquo;");
            m_entityTable.Add(8364, "&euro;");
        }

        /// <summary>
        /// Check if a string IS null or empty
        /// </summary>
        /// <param name="str">string to test</param>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Checks whether a string is NOT null or empty
        /// </summary>
        /// <param name="str">string to test</param>
        public static bool IsNotEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }
        /// <summary>
        /// Get a substring from the start of a string
        /// </summary>
        public static string Left(this string text, int count)
        {
            return count >= text.Length 
                ? text 
                : text.Substring(0, count);
        }

        /// <summary>
        /// Get a substring from the end of a string
        /// </summary>
        public static string Right(this string text, int count)
        {
            int startIndex = text.Length - count;
            return startIndex <= 0 
                ? text 
                : text.Substring(startIndex, count);
        }

        /// <summary>
        /// Find out if a string contains a numeric value
        /// </summary>
        public static bool IsNumeric(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            double d;
            return double.TryParse(text, out d);
        }

        /// <summary>
        /// Filter a string, removing all characters which do not match 
        /// the supplied predicate
        /// </summary>
        public static string Filter(this string text, Predicate<char> match)
        {
            var sb = new StringBuilder();

            foreach (char ch in text.Where(ch => match(ch)))
            {
                sb.Append(ch);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Split the string using the supplied separators, and optionally remove empty entries
        /// </summary>
        public static string[] Split(this string text, bool removeEmptyEntries, params char[] separators)
        {
            return text.Split(separators, 
                removeEmptyEntries 
                    ? StringSplitOptions.RemoveEmptyEntries 
                    : StringSplitOptions.None
                );
        }

        public static decimal? ToDecimal(this string text)
        {
            decimal d;
            if (decimal.TryParse(text, out d))
                return d;
            return null;
        }
        public static int? ToInt32(this string text)
        {
            int d;
            if (int.TryParse(text, out d))
                return d;
            return null;
        }
        public static DateTime? ToDateTime(this string text)
        {
            DateTime d;
            if (DateTime.TryParse(text, out d))
                return d;
            return null;
        }
        public static Guid? ToGuid(this string text)
        {
            if (text.IsGuid())
                return new Guid(text);
            return null;
        }

    }
}