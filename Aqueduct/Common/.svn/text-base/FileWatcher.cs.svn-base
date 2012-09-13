using System;
using System.IO;
using Aqueduct.Diagnostics;

namespace Aqueduct.Common
{
    public class FileWatcher
    {
        private readonly TimeSpan _delayNotificationBy = TimeSpan.Zero;
        private readonly TimeSpan _eventTriggerInterval = TimeSpan.Zero;
        private readonly string _filter = "*.*";
        private readonly NotifyFilters _notifyFilter = GetAllFilters ();
        private readonly string _watchedFolder;
        private DateTime _changedLastTriggeredOn = DateTime.MinValue;
        private DateTime _createdLastTriggeredOn = DateTime.MinValue;
        private DateTime _deletedLastTriggeredOn = DateTime.MinValue;
        private DateTime _renamedLastTriggeredOn = DateTime.MinValue;
        private FileSystemWatcher _watcher;
        readonly ILogger _logger = AppLogger.GetNamedLogger(typeof(FileWatcher));

        public string Filter                        
        {
            get { return _filter; }
        }

        public NotifyFilters NotifyFilter
        {
            get { return _notifyFilter; }
        }

        public TimeSpan EventTriggerInterval
        {
            get { return _eventTriggerInterval; }
        }

        public TimeSpan DeyalNotificationBy
        {
            get { return _delayNotificationBy; }
        }

        private bool LastEventNotTriggeredWithinInterval (DateTime now, DateTime lastTriggeredOn)
        {
            return (now - lastTriggeredOn) > EventTriggerInterval;
        }

        public bool IsStarted
        {
            get { return _watcher.EnableRaisingEvents; }
        }


        private static NotifyFilters GetAllFilters ()
        {
            return NotifyFilters.Attributes & NotifyFilters.CreationTime
                   & NotifyFilters.DirectoryName & NotifyFilters.FileName
                   & NotifyFilters.LastAccess & NotifyFilters.LastWrite
                   & NotifyFilters.Security & NotifyFilters.Size;
        }

        public void Start ()
        {
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop ()
        {
            _watcher.EnableRaisingEvents = false;
        }

        #region Events

        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Renamed;
        public event FileSystemEventHandler Deleted;
        public event FileSystemEventHandler Created;

        #region OnChanged

        /// <summary>
        /// Triggers the Changed event.
        /// </summary>
        public virtual void OnChanged(FileSystemEventArgs ea)
        {
            DateTime now = DateTime.Now;
            if (Changed != null && LastEventNotTriggeredWithinInterval(now, _changedLastTriggeredOn))
            {
                _logger.LogDebugMessage("Triggering onChanged at " + now.ToString());
                _changedLastTriggeredOn = now;
                new DelayedExecutor(() => Changed(this, ea), _delayNotificationBy);
            }
        }

        #endregion

        #region OnRenamed

        /// <summary>
        /// Triggers the Renamed event.
        /// </summary>
        public virtual void OnRenamed(FileSystemEventArgs ea)
        {
            DateTime now = DateTime.Now;
            if (Renamed != null && LastEventNotTriggeredWithinInterval(now, _renamedLastTriggeredOn))
            {
                _logger.LogDebugMessage("Triggering onRenamed at " + now.ToString());
                _renamedLastTriggeredOn = now;
                new DelayedExecutor(() => Renamed(this, ea), _delayNotificationBy);
            }
        }

        #endregion

        #region OnDeleted

        /// <summary>
        /// Triggers the Deleted event.
        /// </summary>
        public virtual void OnDeleted(FileSystemEventArgs ea)
        {
            DateTime now = DateTime.Now;
            if (Deleted != null && LastEventNotTriggeredWithinInterval(now, _deletedLastTriggeredOn))
            {
                _logger.LogDebugMessage("Triggering OnDeleted at " + now.ToString());
                _deletedLastTriggeredOn = now;
                new DelayedExecutor(() => Deleted(this, ea), _delayNotificationBy);
            }
        }

        #endregion

        #region OnCreated

        /// <summary>
        /// Triggers the Created event.
        /// </summary>
        public virtual void OnCreated(FileSystemEventArgs ea)
        {
            DateTime now = DateTime.Now;
            if (Created != null && LastEventNotTriggeredWithinInterval(now, _createdLastTriggeredOn))
            {
                _logger.LogDebugMessage("Triggering OnCreated at " + now.ToString());
                _createdLastTriggeredOn = now;

                new DelayedExecutor(() => Created(this, ea), _delayNotificationBy);
            }
        }

        #endregion

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the FileWatcher class.
        /// </summary>
        /// <param name="watchedFolder"></param>
        public FileWatcher (string watchedFolder)
            : this (watchedFolder, "*.*")
        {
        }

        /// <summary>
        /// Initializes a new instance of the FileWatcher class.
        /// </summary>
        public FileWatcher (string watchedFolder, string filter)
            : this (watchedFolder,
                    filter,
                    GetAllFilters (),
                    TimeSpan.Zero,
                    TimeSpan.Zero)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FileWatcher class.
        /// </summary>
        public FileWatcher (string watchedFolder,
                            string filter,
                            NotifyFilters notifyFilter,
                            TimeSpan eventTriggerInterval, TimeSpan delayNotificationBy)
        {
            _watchedFolder = watchedFolder;
            _filter = filter;
            _notifyFilter = notifyFilter;
            _eventTriggerInterval = eventTriggerInterval;
            _delayNotificationBy = delayNotificationBy;
            ConfigureWatcher ();
        }

        private void ConfigureWatcher ()
        {
            _watcher = new FileSystemWatcher (_watchedFolder)
                       {
                           Filter = _filter,
                           NotifyFilter = _notifyFilter,
                           IncludeSubdirectories = false
                       };
            _watcher.Changed += (sender, e) => OnChanged (e);
            _watcher.Renamed += (sender, e) => OnRenamed (e);
            _watcher.Created += (sender, e) => OnCreated (e);
            _watcher.Deleted += (sender, e) => OnDeleted (e);
        }

        #endregion
    }
}