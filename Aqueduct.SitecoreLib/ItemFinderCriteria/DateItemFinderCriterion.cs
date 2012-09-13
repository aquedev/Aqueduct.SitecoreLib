using System;
using Sitecore.Data.Items;
using Aqueduct.Extensions;

namespace Aqueduct.SitecoreLib.ItemFinderCriteria
{
    public class DateItemFinderCriterion : IItemFinderCriterion
    {
        private readonly string m_fieldName;
        private readonly DateTime m_endDate;
        private readonly DateTime m_startDate;

        public DateItemFinderCriterion(string fieldName, DateTime startDate, DateTime endDate)
        {
            m_fieldName = fieldName;
            m_endDate = endDate;
            m_startDate = startDate;
        }

        public bool Match(Item item)
        {
            DateTime? updated = null;
            if (item[m_fieldName].IsNotEmpty())
                updated = item.GetDateTime(m_fieldName);

            return updated.HasValue && (updated.Value >= m_startDate && updated.Value <= m_endDate);
        }
    }
}


