using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Linq;
using Aqueduct.Common;
using Aqueduct.Common.Context;
using Aqueduct.Configuration.Validators;
using Aqueduct.Diagnostics;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration.Loaders.Xml
{
    public class XmlConfigurationLoader : ConfigurationLoader
    {
        private readonly string _filePath = String.Empty;
        private MemoryStream _configStream;
        private readonly object _lock = new object();
        private FileChangeNotifier _watcher;
        ILogger _logger = AppLogger.GetNamedLogger(typeof(XmlConfigurationLoader));

        private bool HasFile
        {
            get { return _filePath.IsNullOrEmpty() == false; }
        }

        #region .ctors
        public XmlConfigurationLoader(IContext context, string filePath, List<ISettingValidator> validators,
                                       Dictionary<string, Type> typeAliases)
            : base(context, validators, typeAliases)
        {
            _filePath = context.ResolvePath(filePath);
            InitialiseFileWatcher();
        }

        public XmlConfigurationLoader(IContext context, string filePath, List<ISettingValidator> validators)
            : this(context, filePath, validators, new Dictionary<string, Type>())
        {
        }

        public XmlConfigurationLoader(IContext context, string filePath)
            : this(context, filePath, new List<ISettingValidator>())
        {
        }

        public XmlConfigurationLoader(IContext context, Stream configStream, List<ISettingValidator> validators,
                                       Dictionary<string, Type> typeAliases)
            : base(context, validators, typeAliases)
        {
            ConfigGuard.ArgumentNotNull(configStream, "configStream", "XmlConfiguration loader requires a non null Stream");

            LoadStream(configStream);
        }


        public XmlConfigurationLoader(IContext context, Stream configStream, List<ISettingValidator> validators)
            : this(context, configStream, validators, new Dictionary<string, Type>())
        {
        }

        public XmlConfigurationLoader(IContext context, Stream configStream)
            : this(context, configStream, new List<ISettingValidator>())
        {
        }

        private void LoadStream(Stream configStream)
        {
            using (configStream)
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                int numBytes = 0;
                _configStream = new MemoryStream();
                while ((numBytes = configStream.Read(buffer, 0, size)) > 0)
                {
                    _configStream.Write(buffer, 0, numBytes);
                }
                ResetConfigStream();
            }
        }

        private void ResetConfigStream()
        {
            _configStream.Position = 0;
        }

        #endregion

        private void InitialiseFileWatcher()
        {
            if (HasFile == false)
                return;

            TimeSpan pauseWatchingInterval = TimeSpan.FromSeconds(2);
            TimeSpan delayNotificationBy = TimeSpan.FromMilliseconds(1000);
            _watcher = new FileChangeNotifier(_filePath);
            _watcher.Changed += (sender, args) => ValidateFileAndRaiseSettingsChanged();
        }

        private void StartWatchingFileForChanges()
        {
            if (_watcher != null && _watcher.IsStarted == false)
                _watcher.Start();
        }

        private void ValidateFileAndRaiseSettingsChanged()
        {
            _logger.LogInfoMessage("NotificationRaised: Updating config");
            if (Monitor.TryEnter(_lock, 100) == false)
                return;

            int retries = 5;
            FileStream stream = null;
            try
            {
                for (int i = 0; i < retries; i++)
                {
                    try
                    {
                        stream = OpenConfigFileForReadWithoutBlocking();
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (retries == 0)
                            _logger.LogError("Error while trying to open file stream to confirm a change has occurred", ex);

                        stream = null;
                        Thread.Sleep(250);
                    }
                }

                if (stream != null)
                {
                    stream.Close();
                    OnSettingsChanged();
                    _logger.LogInfoMessage("Config updated successfully");
                }
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                Monitor.Exit(_lock);
            }
        }

        protected override void LoadSections()
        {
            _sections.Clear();
            LoadSectionsFromXML();
            StartWatchingFileForChanges();
        }

        private void LoadSectionsFromXML()
        {
            ValidateConfigFile();
            XDocument doc = LoadDocument();
            var docNamespace = doc.Root.GetDefaultNamespace();
            foreach (var element in doc.Root.Elements(docNamespace + "section"))
            {
                Section section = XmlSectionParser.Parse(element);
                section.AddSettings(LoadSectionSettings(element));
                _sections.Add(section);
            }
        }

        private XDocument LoadDocument()
        {
            XDocument document;
            if (HasFile)
            {
                using (FileStream stream = OpenConfigFileForReadWithoutBlocking())
                using (StreamReader reader = new StreamReader(stream))
                    document = XDocument.Load(reader);
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(_configStream))
                {
                    document = XDocument.Load(reader);
                    ResetConfigStream();
                }
            }

            return document;
        }



        protected ISettingsList LoadSectionSettings(XElement sectionElement)
        {
            SettingsList settings = new SettingsList();

            XNamespace sectionNamespace = sectionElement.GetDefaultNamespace();
            var settingsList = from settingNode in sectionElement.Descendants(sectionNamespace + "setting")
                               select CreateSetting(settingNode);

            foreach (var setting in settingsList)
            {
                settings.Add(setting);
            }

            return settings;
        }


        private Setting CreateSetting(XElement settingNode)
        {
            string key = settingNode.AttributeValue("key");
            string type = settingNode.AttributeValue("type");
            string valueStr = GetSettingsValue(settingNode);

            return new Setting(key, valueStr, ConvertType(type));
        }

        private static string GetSettingsValue(XElement settingNode)
        {
            string valueStr = settingNode.AttributeValue("value");
            if (valueStr.IsNullOrEmpty())
            {
                if (settingNode.HasElements)
                {
                    var firstChild = settingNode.FirstNode;

                    if (firstChild is XCData)
                        valueStr = settingNode.Value;
                    else
                        valueStr = settingNode.InnerXml();
                }
            }
            return valueStr;
        }
        private void ValidateConfigFile()
        {
            XmlDocument document = new XmlDocument();

            XmlReaderSettings settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, CloseInput = HasFile };
            settings.Schemas.Add(GetSchema());
            if (HasFile)
            {
                using (XmlReader reader = XmlReader.Create(OpenConfigFileForReadWithoutBlocking(), settings))
                    document.Load(reader);
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(_configStream, settings))
                    document.Load(reader);
                ResetConfigStream();
            }
        }

        private FileStream OpenConfigFileForReadWithoutBlocking()
        {
            return File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        private static XmlSchema GetSchema()
        {
            using (Stream schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Aqueduct.Configuration.Schema.Schema.xsd"))
            {
                return XmlSchema.Read(schemaStream, null);
            }
        }
    }
}
