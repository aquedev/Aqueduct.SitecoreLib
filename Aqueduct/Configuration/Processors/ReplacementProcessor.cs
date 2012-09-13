using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aqueduct.Configuration.Processors
{
    public class ReplacementProcessor : ISettingsProcessor
    {
        private const string PATTERN = @"\$\{(([A-z0-9\-\.]*(\$\{[A-z0-9\-\.\$\{\}]+\})?[A-z0-9\-\.]*)+)\}";
        private readonly Regex placeholderRegex;
        private readonly List<Setting> processedList = new List<Setting>();
        private ISettingsList _settings;

        public ReplacementProcessor()
        {
            placeholderRegex = new Regex(PATTERN, RegexOptions.Singleline);
        }


        public void Process(ISettingsList settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings", "Processor cannot process an empty collection of settings.");

            _settings = settings;
            foreach (Setting setting in _settings.Values)
            {
                processedList.Clear();
                ProcessSetting(setting);
            }
        }

        private void ProcessSetting(Setting set)
        {
            if (set.IsProcessed)
                return;

            processedList.Add(set);
            set.Raw = FindAndProcessReplacements(set.Raw);
            set.IsProcessed = true;
        }

        private string FindAndProcessReplacements(string original)
        {
            string processedString = original;

            Match match = Match.Empty;
            while ((match = placeholderRegex.Match(processedString)).Success)
            {
                string processedMatch = ProcessPlaceholder(match);
                processedString = ReplaceMatch(processedString, match, processedMatch);
            }

            return processedString;
        }

        private string ProcessPlaceholder(Match placeholderMatch)
        {
            string placeholderString = placeholderMatch.Groups[1].Value;

            placeholderString = FindAndProcessReplacements(placeholderString);

            EnsureThereAreNoCircularReplacements(placeholderString);

            return GetReplacement(placeholderString);
        }

        private void EnsureThereAreNoCircularReplacements(string matchKey)
        {
            if (processedList.Count(set => set.Key == matchKey) > 0)
                throw new CircularReplacementException(
                    String.Format("Key {0} is in the list of keys being processed.", matchKey), processedList);
        }

        private string GetReplacement(string placeholderString)
        {
            Setting replacement = _settings[placeholderString];
            if (replacement != null)
            {
                if (!replacement.IsProcessed)
                    ProcessSetting(replacement);

                return replacement.Raw;
            }
            else
                throw new SettingNotFoundException(placeholderString);
        }

        private static string ReplaceMatch(string original, Match match, string replacement)
        {
            string result = original.Substring(0, match.Index);
            result += replacement;
            result += original.Substring(match.Index + match.Length);
            return result;
        }

        

        
    }
}