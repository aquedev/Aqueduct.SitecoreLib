using System;
using Aqueduct.Common.Context;
using Aqueduct.Configuration.Validators;

namespace Aqueduct.Configuration.Loaders
{
    public interface IConfigurationLoader
    {
        string Version { get; }
        IContext Context { get; }
        SectionList Load ();

        void AddValidator (ISettingValidator validator);

        event EventHandler SettingsChanged;
    }
}