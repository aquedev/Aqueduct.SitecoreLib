using System;
using System.Threading;
using System.IO;
using Aqueduct.Diagnostics;

namespace Aqueduct.Common
{
    public sealed class FileChangeNotifier : IDisposable
    {
        private string m_pathToWatch;
        private Timer m_timer;
        private ILogger m_log;

        private const int TimeoutMillis = 500;

        private FileSystemWatcher m_watcher;

        public bool IsStarted
        {
            get { return m_watcher.EnableRaisingEvents; }
        }

        public event EventHandler Changed;

        #region OnChanged
        private void OnChanged()
        {
            if (Changed != null)
                Changed(null, new EventArgs());
        }
        #endregion

        public FileChangeNotifier(string file)
        {
            m_log = AppLogger.GetNamedLogger(typeof(FileChangeNotifier));

            m_pathToWatch = file;

            // Create a new FileSystemWatcher and set its properties.
            m_watcher = new FileSystemWatcher();

            m_watcher.Path = Path.GetDirectoryName(m_pathToWatch);
            m_watcher.Filter = Path.GetFileName(m_pathToWatch);

            // Set the notification filters
            m_watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;

            // Add event handlers. OnChanged will do for all event handlers that fire a FileSystemEventArgs
            m_watcher.Changed += new FileSystemEventHandler(ConfigFileWatcher_OnChanged);
            m_watcher.Created += new FileSystemEventHandler(ConfigFileWatcher_OnChanged);
            m_watcher.Deleted += new FileSystemEventHandler(ConfigFileWatcher_OnChanged);

            // Create the timer that will be used to deliver events. Set as disabled
            m_timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
            m_log.LogInfoMessage("FileChangeNotifier Initialised");
        }

        public void Start()
        {
            m_watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            m_watcher.EnableRaisingEvents = false;
        } 

        private void ConfigFileWatcher_OnChanged(object source, FileSystemEventArgs e)
        {
            m_timer.Change(TimeoutMillis, Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            m_log.LogInfoMessage(String.Format("Raising Changed event for file {0}", m_pathToWatch));
            OnChanged();
        }

        public void Dispose()
        {
            m_watcher.EnableRaisingEvents = false;
            m_watcher.Dispose();
            m_timer.Dispose();
        }
    }
}
