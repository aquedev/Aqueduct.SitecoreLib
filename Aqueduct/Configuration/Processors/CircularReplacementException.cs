using System;
using System.Collections.Generic;

namespace Aqueduct.Configuration.Processors
{
    /// <summary>
    /// This is used by the replacement processor
    /// </summary>
    [Serializable]
    public class CircularReplacementException : Exception
    {
        private List<Setting> _processedList;

        public CircularReplacementException (string message, List<Setting> processedList) : base (message)
        {
            _processedList = processedList;
        }

    }
}