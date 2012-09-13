using System;
using System.Threading;

namespace Aqueduct.Common
{
    public class DelayedExecutor
    {
        private readonly Action _methodToExecute;
        private readonly Timer _timer;

        public DelayedExecutor (Action methodToExecute, TimeSpan delay)
        {
            if (methodToExecute == null)
                throw new ArgumentNullException ("methodToExecute",
                                                 "Cannot create a DelayedExecutor if methodToExecute is null");
            _methodToExecute = methodToExecute;

            _timer = new Timer (DisableTimeAndExecuteEvent, null, TimeSpan.Zero, delay);
        }

        private void DisableTimeAndExecuteEvent (object state)
        {
            _timer.Change (Timeout.Infinite, Timeout.Infinite);
            try
            {
            	_methodToExecute.Invoke ();
            }
            finally
            {
            	_timer.Dispose();
            }
        }
    }
}