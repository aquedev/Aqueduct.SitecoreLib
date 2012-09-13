using Aqueduct.Common.Context;

namespace Aqueduct.Configuration.Loaders
{
    public class SettingsConfigurationLoader : ConfigurationLoader
    {
        private SettingsList _predefinedSettings;
        public SettingsConfigurationLoader(SettingsList predefinedSettings)
            : this(predefinedSettings, NullContext.Instance) { }

        public SettingsConfigurationLoader(SettingsList predefinedSettings, IContext context)
            : base(context)
        {
            _predefinedSettings = predefinedSettings;
        }

        protected override void LoadSections()
        {
            Section globalSection = new Section("Global");
            globalSection.AddSettings(_predefinedSettings);
            _sections.Add(globalSection);
        }
    }
}
