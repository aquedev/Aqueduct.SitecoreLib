using System.Collections.Generic;
using Sitecore.Data.Items;
using System.Linq;

namespace Aqueduct.SitecoreLib.ItemFinderCriteria
{
    public class AndCompositeItemFinderCriterion : IItemFinderCriterion
    {
        private IEnumerable<IItemFinderCriterion> m_criteria;

        public AndCompositeItemFinderCriterion(IEnumerable<IItemFinderCriterion> criteria)
        {
            m_criteria = criteria;
        }

        public bool Match(Item item)
        {
            return m_criteria.Aggregate(true, (acc, criterion) => acc && criterion.Match(item));
        }
    }

    public class OrCompositeItemFinderCriterion : IItemFinderCriterion
    {
        private IEnumerable<IItemFinderCriterion> m_criteria;

        public OrCompositeItemFinderCriterion(IEnumerable<IItemFinderCriterion> criteria)
        {
            m_criteria = criteria;
        }

        public bool Match(Item item)
        {
            return m_criteria.Aggregate(false, (acc, criterion) => acc || criterion.Match(item));
        }
    }
}


