using System;

namespace Aqueduct.Configuration.Loaders
{
    public class SettingEventArgs : EventArgs
    {
        private Setting m_setting;

        /// <summary>
        /// Initializes a new instance of the SettingEventArgs class.
        /// </summary>
        /// <param name="m_setting"></param>
        public SettingEventArgs (Setting m_setting)
        {
            this.m_setting = m_setting;
        }

        public bool Cancel { get; set; }
    }
}