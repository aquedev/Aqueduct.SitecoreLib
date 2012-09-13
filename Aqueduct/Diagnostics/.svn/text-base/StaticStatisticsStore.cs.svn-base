using System.Collections.Generic;

namespace Aqueduct.Diagnostics
{
    public class StaticStatisticsStore : BaseStatisticsStore
    {
        private IDictionary<string, long> m_store = new Dictionary<string, long>();

        protected override IDictionary<string, long> Dictionary
        {
            get { return m_store; }
        }

        public override bool IsActive()
        {
            return true;
        }
    }
}
