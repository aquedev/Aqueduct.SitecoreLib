namespace Aqueduct.Configuration.Processors
{
    public interface ISettingsProcessor
    {
        void Process (ISettingsList settings);
    }
}