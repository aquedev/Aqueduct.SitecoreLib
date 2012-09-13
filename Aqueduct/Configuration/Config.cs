using System;
using System.Collections.Generic;
using System.Linq;
using Aqueduct.Common;
using Aqueduct.Common.Context;
using Aqueduct.Configuration.Loaders;
using Aqueduct.Configuration.Loaders.Xml;
using Aqueduct.Configuration.Validators;

namespace Aqueduct.Configuration
{
    public sealed class Config
    {
        private static ConfigurationHandler _handler;

        public static event EventHandler SettingsChanged;
        
        private static bool _isInitialized;

        public static IConfigurationHandler ConfigurationHandler
        {
            get {
                //ValidateConfigIsInitialized();
                return _handler; 
            }
        }

        public static Dictionary<string, object> AllSettings
        {
            get {
                ValidateConfigIsInitialized ();
                return _handler.AllSettings;
            }
        }

        public static ISettingsList Settings
        {
            get {
                ValidateConfigIsInitialized();
                return _handler.Settings; }
        }

        public static void Initialize (IContext context)
        {
            Initialize (new XmlConfigurationLoader (context,
                                                    new ConfigurationFileLocator (context).GetConfigurationFile ()));
        }

        public static void Initialize(IConfigurationLoader loader)
        {
            Section applicationSection = GetApplicationSection(loader.Context);
            loader.AddValidator(GetApplicationSectionValidator(applicationSection));
            _handler = new ConfigurationHandler(loader, new SectionList { applicationSection });
            _handler.LoadSettings();
            _handler.SettingsChanged += HandleSettingsChanged;
            _isInitialized = true;
        }

        static void HandleSettingsChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }

        private static ISettingValidator GetApplicationSectionValidator(Section applicationSection)
        {
            return new OverridesValidator(applicationSection.Keys.ToArray());
        }

        private static Section GetApplicationSection(IContext context)
        {
            Section applicationSection = new Section("__ApplicationSection");
            applicationSection.Add(new Setting("Application.Mode", context.AppMode.ToString(), context.AppMode,
                                                 typeof(ApplicationMode)));
            applicationSection.Add(new Setting("Application.Path", context.ResolvePath(""), typeof(string)));
            applicationSection.Add(new Setting("Application.ServerName", context.ServerName, typeof(string)));

            return applicationSection;
        }

        private static void ValidateConfigIsInitialized ()
        {
            if (!_isInitialized)
                throw new InvalidOperationException ("Config.Initialize needs to be called before use");
        }

        public static class Globals
        {
            public static string ApplicationPath
            {
                get { return Config.Get("Application.Path"); }
            }
            public static ApplicationMode ApplicationMode
            {
                get { return Config.Get<ApplicationMode>("Application.Mode"); }
            }
        }

        public static string Get (string key)
        {
            ValidateConfigIsInitialized ();
            return _handler.Get (key);
        }

        public static string Get (string key, string defaultValue)
        {
            ValidateConfigIsInitialized ();
            return _handler.Get (key, defaultValue);
        }

        public static T Get<T> (string key)
        {
            ValidateConfigIsInitialized ();
            return _handler.Get<T> (key);
        }

        public static T Get<T> (string key, T defaultValue)
        {
            ValidateConfigIsInitialized ();
            return _handler.Get (key, defaultValue);
        }

        public static Dictionary<string, object> GetAll (Func<string, bool> keyFunction)
        {
            ValidateConfigIsInitialized ();
            return _handler.GetAll (keyFunction);
        }

        //Helper methods
        public static IConfigurationHandler CreateConfig(Dictionary<string, object> settings)
        {
            Section globalSection = new Section("Global");
            foreach(KeyValuePair<string, object> pair in settings)
            {
                globalSection.Add(Setting.CreateProcessedSetting(pair.Key, pair.Value));
            }
            var sections = new SectionList() {globalSection };
            ConfigurationHandler config = new ConfigurationHandler(new SettingsConfigurationLoader(globalSection));
            config.LoadSettings();
            return config;
        }
    }
}