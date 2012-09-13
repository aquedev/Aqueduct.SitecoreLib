using System;
using System.Collections.ObjectModel;
using System.Linq;
using Aqueduct.Common;
using Aqueduct.Common.Context;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration
{
    public class Section : SettingsList
    {
        public const string GlobalSectionName = "Global";

        private bool _isGlobal;

        private string _Name;

        public virtual bool IsNull
        {
            get { return false; }
        }

        public bool IsGlobal
        {
            get { return _isGlobal; }
        }

        public virtual string Name
        {
            get { return _Name; }
            protected set
            {
                if (value.IsNotEmpty () && value.Equals (GlobalSectionName, StringComparison.CurrentCultureIgnoreCase))
                    _isGlobal = true;

                _Name = value;
            }
        }

        public virtual ApplicationMode Mode { get; protected set; }
        public virtual ReadOnlyCollection<string> Servers { get; protected set; }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Section class.
        /// </summary>
        protected Section ()
        {
        }

        public Section (string name)
            : this (name, ApplicationMode.Auto)
        {
        }

        public Section (string name, ApplicationMode mode)
            : this (name, mode, new string[0])
        {
        }

        public Section (string name, ApplicationMode mode, string[] servers)
        {
            Mode = mode;
            Name = name;
            Servers = new ReadOnlyCollection<string> (servers);
        }

        #endregion

        public virtual bool IsActive (IContext context)
        {
            return ApplicationModeIsAutoOrMatches (context.AppMode) && ServerListIsEmptyOrContains (context.ServerName);
        }

        private bool ApplicationModeIsAutoOrMatches (ApplicationMode applicatioMode)
        {
            return Mode == applicatioMode || Mode == ApplicationMode.Auto;
        }

        private bool ServerListIsEmptyOrContains (string serverName)
        {
            return Servers.Count == 0 || (serverName.IsNotEmpty() &&
                Servers.Contains (serverName, new CurrentCultureIgnoreCaseEqualityComparer ()));
        }
    }
}