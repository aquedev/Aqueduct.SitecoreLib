using System;
using Aqueduct.Common.Context;
using Aqueduct.Configuration;
using Aqueduct.Configuration.Loaders;
using Aqueduct.Configuration.Validators;
using Aqueduct.SitecoreLib.EventHandlers;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib
{
	public class SitecoreConfigLoader : IConfigurationLoader
	{
		private readonly Database _database;
		private readonly string _settingsRoot;

		public SitecoreConfigLoader(string settingsRoot, Database database)
		{
			_settingsRoot = settingsRoot;
			_database = database;
            SitecoreEvents.OnPublishEnd += ((sender, e) => InvokeSettingsChanged(e));
		}

		#region IConfigurationLoader Members

		public SectionList Load()
		{
			var sectionList = new SectionList();
			var globalSection = new Section("Global");
			Item settingsRoot = _database.GetItem(_settingsRoot);
			sectionList.Add(GetSettings(settingsRoot, globalSection, settingsRoot.Name));
			return sectionList;
		}


		public void AddValidator(ISettingValidator validator)
		{
		}

		public string Version
		{
			get { return "1.0"; }
		}

		public IContext Context
		{
			get { return NullContext.Instance; }
		}

		public event EventHandler SettingsChanged;

	    private void InvokeSettingsChanged(EventArgs e)
	    {
	        EventHandler handler = SettingsChanged;
	        if (handler != null) handler(this, e);
	    }

	    #endregion

		private static Section GetSettings(Item item, Section globalSection, string name)
		{			
			foreach (Item childItem in item.Children)
			{
				string pathName = name + "." + childItem.Name;
				if (childItem.TemplateName.Equals("Folder"))
				{
					GetSettings(childItem, globalSection, pathName);
				}
				else
				{
					globalSection.Add(new Setting(pathName, childItem.Fields["Value"].ToString(), typeof(string)));
				}
			}

			return globalSection;
		}
	}
}