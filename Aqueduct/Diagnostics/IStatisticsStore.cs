using System.Collections.Generic;

namespace Aqueduct.Diagnostics
{
    public interface IStatisticsStore
    {
        bool IsActive();
        void Clear();
        void Increment(string name);
        void Increment(string name, long value);
        void SetValueIfHigher(string name, long value);

        IDictionary<string, long> GetStatistics();
    }
}