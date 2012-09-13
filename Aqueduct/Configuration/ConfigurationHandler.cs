using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aqueduct.Configuration.Loaders;
using Aqueduct.Configuration.Parsers;
using Aqueduct.Configuration.Processors;

namespace Aqueduct.Configuration
{
    public class ConfigurationHandler : IConfigurationHandler
    {
        private readonly IConfigurationLoader _loader;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim (LockRecursionPolicy.SupportsRecursion);
        private readonly SettingParsersResolver _parserResolver = new SettingParsersResolver();
        private readonly SectionList _predefinedSections = new SectionList ();
        private readonly List<ISettingsProcessor> _processors = new List<ISettingsProcessor> ();
        private readonly SectionList _sections = new SectionList ();
        private ISettingsList __readOnlySettings;
        private bool _isLoaded;
        private ISettingsList _settings;

        /// <summary>
        /// Whenever the settings have changed this event is raised
        /// </summary>
        public event EventHandler SettingsChanged;

        #region OnSettingsChanged
        protected internal virtual void OnSettingsChanged()
        {
            if (SettingsChanged != null)
                SettingsChanged(this, new EventArgs());
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the ConfigurationHandler class.
        /// </summary>
        /// <param name="loader"></param>
        public ConfigurationHandler (IConfigurationLoader loader)
            : this (loader, new SectionList ())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConfigurationHandler class.
        /// </summary>
        /// <param name="loader"></param>
        public ConfigurationHandler (IConfigurationLoader loader, SectionList predefinedSections)
            : this (loader, predefinedSections, new List<ISettingParser> (), new List<ISettingsProcessor> ())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConfigurationHandler class.
        /// </summary>
        /// <param name="loader"></param>
        public ConfigurationHandler (IConfigurationLoader loader,
                                     SectionList predefinedSections,
                                     List<ISettingParser> settingParsers,
                                     List<ISettingsProcessor> processors)
        {
            _loader = loader;

            _predefinedSections.AddRange (predefinedSections);

            RegisterParsers (settingParsers);
            RegisterProcessors (processors);
            RegisterForLoaderSettingsChanged ();
        }

        private ISettingsList _readOnlySettings
        {
            get { return __readOnlySettings; }
        }

        public Dictionary<string, object> AllSettings
        {
            get
            {
                try
                {
                    _lock.EnterReadLock ();
                    ValidateSettingsAreLoaded ();
                    return _readOnlySettings.ToKeyValueDictionary ();
                }
                finally
                {
                    _lock.ExitReadLock ();
                }
            }
        }

        #region IConfigurationHandler Members

        public string this [string key]
        {
            get { return Get (key); }
        }

        public string Get (string key)
        {
            try
            {
                _lock.EnterReadLock ();
                ValidateSettingsAreLoaded ();

                // todo: add a helper method
                ISettingsList settings = __readOnlySettings;
                if (!settings.ContainsKey (key))
                    throw new KeyNotFoundException (String.Format ("Key \"{0}\" was not found", key));

                Setting result = settings [key];
                return (result.Value == null) ? string.Empty : result.Value.ToString ();
            }
            finally
            {
                _lock.ExitReadLock ();
            }
        }

        public string Get (string key, string defaultValue)
        {
            try
            {
                _lock.EnterReadLock ();
                if (!__readOnlySettings.ContainsKey (key))
                    return defaultValue;

                return Get (key);
            }
            finally
            {
                _lock.ExitReadLock ();
            }
        }

        public T Get<T> (string key)
        {
            try
            {
                _lock.EnterReadLock ();
                ValidateSettingsAreLoaded ();

                ISettingsList settings = _readOnlySettings;
                if (!settings.ContainsKey (key))
                    throw new KeyNotFoundException (String.Format ("Key \"{0}\" was not found", key));

                return (T) settings [key].Value;
            }
            finally
            {
                _lock.ExitReadLock ();
            }
        }

        public T Get<T> (string key, T defaultValue)
        {
            try
            {
                _lock.EnterReadLock ();
                if (!_readOnlySettings.ContainsKey (key))
                    return defaultValue;

                return Get<T> (key);
            }
            finally
            {
                _lock.ExitReadLock ();
            }
        }

        public Dictionary<string, object> GetAll (Func<string, bool> keyFunction)
        {
            try
            {
                _lock.EnterReadLock ();
                ValidateSettingsAreLoaded ();

                Dictionary<string, object> results = new Dictionary<string, object> ();
                ISettingsList settings = _readOnlySettings;
                foreach (string key in AllSettings.Keys.Where (keyFunction))
                {
                    results.Add (key, settings [key]);
                }
                return results;
            }
            finally
            {
                _lock.ExitReadLock ();
            }
        }

        public ISettingsList Settings
        {
            get
            {
                try
                {
                    _lock.EnterReadLock ();
                    ValidateSettingsAreLoaded ();
                    return _readOnlySettings;
                }
                finally
                {
                    _lock.ExitReadLock ();
                }
            }
        }

        #endregion

        protected void RegisterParsers (List<ISettingParser> parsers)
        {
            if (parsers == null)
                throw new ArgumentNullException ("parsers",
                                                 "ConfigurationHandler doesn't accept null to be passed as a parsers collection. Either pass a list of SettingParser or an empty list");

            _parserResolver.Register(new BooleanSettingParser());
            _parserResolver.Register(new SystemTypesSettingParser());
            
            _parserResolver.Register(new GuidSettingParser());
            _parserResolver.Register(new ListSettingParser(_parserResolver));
        }

        private void RegisterProcessors (List<ISettingsProcessor> processors)
        {
            if (processors == null)
                throw new ArgumentNullException ("processors",
                                                 "ConfigurationHandler doesn't accept null to be passed as a processors collection. Either pass a list of SettingsProcessor or an empty list");

            _processors.Add (new ReplacementProcessor ());
            _processors.AddRange (processors);
        }

        public void LoadSettings ()
        {
            try
            {
                _lock.EnterWriteLock ();

                ClearSections ();
                AddPredifinedSections ();

                LoadSections ();

                CombineSections ();
                ProcessSettings ();
                ParseSettings ();
                MakeSettingsReadOnly ();

                _isLoaded = true;
            }
            finally
            {
                _lock.ExitWriteLock ();
            }
        }
            
        private void ClearSections ()
        {
            _sections.Clear ();
        }


        private void AddPredifinedSections ()
        {
            _sections.AddRange (_predefinedSections);
        }

        private void LoadSections ()
        {
            SectionList loadedSections = _loader.Load ();
            CheckForPresenseOfGlobalSection (loadedSections);
            _sections.AddRange (loadedSections);
        }

        private static void CheckForPresenseOfGlobalSection (SectionList loadedSections)
        {
            if (loadedSections == null || loadedSections.Count == 0 || !loadedSections [0].IsGlobal)
                throw new ConfigurationException (
                    "The loaded sections should contain at least one section. The first section should be named 'Global'");
        }

        private void CombineSections ()
        {
            _settings = new SettingsList ();
            foreach (Section section in _sections)
            {
                foreach (Setting setting in section.Values)
                {
                    _settings [setting.Key] = setting;
                }
            }
        }

        private void ProcessSettings ()
        {
            foreach (ISettingsProcessor processor in _processors)
            {
                processor.Process (_settings);
            }
        }

        private void ParseSettings()
        {
            foreach (Setting setting in _settings.Values)
            {
                if (!setting.IsProcessed)
                    throw new SettingNotProcessedException(setting);

                if (setting.Value == null)
                {
                    ISettingParser parser = _parserResolver.Resolve(setting.Type);
                    try
                    {
                        setting.Value = parser.Parse(setting.Raw, setting.Type);
                    }
                    catch (Exception ex)
                    {
                        string formattedExceptionMessage = String.Format("Cannot parse settting with key:[{0}] and value:[{1}]. Please see inner expection for details.", setting.Key, setting.Value);
                        throw new ConfigurationException(formattedExceptionMessage, ex);
                    }
                    ;
                }
            }
        }

        private void MakeSettingsReadOnly ()
        {
            __readOnlySettings = _settings.ToReadOnly ();
        }

        private void RegisterForLoaderSettingsChanged ()
        {
            _loader.SettingsChanged += (sender, args) => { LoadSettings(); OnSettingsChanged(); };
        }

        private void ValidateSettingsAreLoaded ()
        {
            ConfigGuard.ThrowInvalidOperation (() => !_isLoaded, "The config hasn't been initialised. Call Config.Initialise if you are using the global config or LoadSettings if you have created a separate instance.");
        }
    }
}