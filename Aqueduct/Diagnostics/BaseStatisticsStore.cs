using System.Collections.Generic;
using System.Linq;

namespace Aqueduct.Diagnostics
{
    public abstract class BaseStatisticsStore : IStatisticsStore
    {
        protected abstract IDictionary<string, long> Dictionary { get; }

        #region IStatisticsStore Members

        public abstract bool IsActive();

        public virtual void Clear()
        {
            Dictionary.Clear();
        }

        public void Increment(string name, long value)
        {
            if (Dictionary.ContainsKey(name))
                Dictionary[name] += value;
            else
                Dictionary.Add(name, value);
        }

        public void Increment(string name)
        {
            Increment(name, 1);
        }

        public void SetValueIfHigher(string name, long value)
        {
            if (Dictionary.ContainsKey(name))
            {
                long oldValue = Dictionary[name];
                if (value > oldValue)
                    Dictionary[name] = value;
            }
            else
            {
                Dictionary[name] = value;
            }
        }

        public IDictionary<string, long> GetStatistics()
        {
            //Clone the Dictionary to stop people messing with the stats
            return Dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        #endregion
    }
}
