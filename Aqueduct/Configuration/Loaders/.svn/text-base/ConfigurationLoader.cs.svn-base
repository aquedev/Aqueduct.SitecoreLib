using System;
using System.Collections.Generic;
using Aqueduct.Common.Context;
using Aqueduct.Configuration.Converter;
using Aqueduct.Configuration.Validators;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration.Loaders
{
    public abstract class ConfigurationLoader : IConfigurationLoader
    {
        public const string DEFAULT_TYPE = "string";
        protected readonly IContext _context;
        private readonly TypeConverter _typeConverter;
        private readonly List<ISettingValidator> _validators = new List<ISettingValidator> ();
        protected SectionList _sections = new SectionList ();
        private string _version = "1.0";

        public ConfigurationLoader (IContext context)
            : this (context, new List<ISettingValidator> ())
        {
        }

        public ConfigurationLoader (IContext context, List<ISettingValidator> validators)
            : this (context, validators, new Dictionary<string, Type> ())
        {
        }

        public ConfigurationLoader (IContext context, List<ISettingValidator> validators,
                                    Dictionary<string, Type> typeAliases)
        {
            _context = context;
            _validators = validators;
            _typeConverter = new TypeConverter (typeAliases);
        }

        #region IConfigurationLoader Members

        public string Version
        {
            get { return _version; }
            protected set { _version = value; }
        }

        public IContext Context
        {
            get { return _context; }
        }

        public void AddValidator (ISettingValidator validator)
        {
            _validators.Add (validator);
        }

        public SectionList Load ()
        {
            _sections.Clear ();
            LoadSections ();
            ForEachSetting (ValidateSetting);
            return GetActiveSections ();
        }

        #endregion

        public Type ConvertType (string type)
        {
            if (type.IsNullOrEmpty())
                return typeof(string);

            return _typeConverter.ParseType (type);
        }
        
        protected abstract void LoadSections ();

        private void ForEachSetting (Action<Setting> action)
        {
            foreach (Section section in _sections)
                foreach (Setting setting in section.Values)
                    action.Invoke (setting);
        }

        private void ValidateSetting (Setting setting)
        {
            try
            {
                foreach (ISettingValidator validator in _validators)
                    validator.Validate (setting);
            }
            catch (ValidationException ex)
            {
                throw new ConfigurationException (
                    String.Format ("Setting {0} doesn't validate. Validator failed with message {1}",
                                   setting.Key, ex.Message));
            }
        }

        private SectionList GetActiveSections ()
        {
            SectionList activeSections = new SectionList ();
            foreach (Section section in _sections)
            {
                if (section.IsActive (_context))
                    activeSections.Add (section);
            }
            return activeSections;
        }

        public event EventHandler SettingsChanged;

        public event EventHandler<SettingEventArgs> SettingLoading;

        public event EventHandler<SettingEventArgs> SettingLoaded;

        public virtual void OnSettingsChanged()
        {
            if (SettingsChanged != null)
                SettingsChanged(this, new EventArgs());
        }

        public virtual void OnSettingLoaded (SettingEventArgs ea)
        {
            if (SettingLoaded != null)
                SettingLoaded (null /*this*/, ea);
        }


        public virtual void OnSettingLoading (SettingEventArgs ea)
        {
            if (SettingLoading != null)
                SettingLoading (null /*this*/, ea);
        }
    }
}