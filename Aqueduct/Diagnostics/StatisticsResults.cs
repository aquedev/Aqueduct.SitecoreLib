using System.Collections.Generic;

namespace Aqueduct.Diagnostics
{
    public class StatisticsResults
    {
        public string Name { get; private set; }
        public IDictionary<string, long> Store { get; private set; }

        public StatisticsResults(string name, IDictionary<string, long> store)
        {
            Name = name;
            Store = store;
        }
    }
}
