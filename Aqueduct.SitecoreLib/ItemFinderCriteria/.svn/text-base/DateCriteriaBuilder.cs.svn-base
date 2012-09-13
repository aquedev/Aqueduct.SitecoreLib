using System;
using System.Collections.Generic;

namespace Aqueduct.SitecoreLib.ItemFinderCriteria
{
    public class DateCriteriaBuilder
    {
        private static string UPDATE_FIELD_NAME = "__Updated";
        private static string CREATE_FIELD_NAME = "__Created";

        private List<IItemFinderCriterion> m_criteria = new List<IItemFinderCriterion>();

        public DateCriteriaBuilder CreatedBetween(DateTime startDate, DateTime endDate)
        {
            m_criteria.Add(new DateItemFinderCriterion(CREATE_FIELD_NAME, startDate, endDate));
            return this;
        }

        public DateCriteriaBuilder CreatedAfter(DateTime startDate)
        {
            m_criteria.Add(new DateItemFinderCriterion(CREATE_FIELD_NAME, startDate, DateTime.MaxValue));
            return this;
        }

        public DateCriteriaBuilder CreatedBefore(DateTime endDate)
        {
            m_criteria.Add(new DateItemFinderCriterion(CREATE_FIELD_NAME, DateTime.MinValue, endDate));
            return this;
        }

        public DateCriteriaBuilder UpdatedBetween(DateTime startDate, DateTime endDate)
        {
            m_criteria.Add(new DateItemFinderCriterion(UPDATE_FIELD_NAME, startDate, endDate));
            return this;
        }

        public DateCriteriaBuilder UpdatedBefore(DateTime endDate)
        {
            m_criteria.Add(new DateItemFinderCriterion(UPDATE_FIELD_NAME, DateTime.MinValue, endDate));
            return this;
        }

        public DateCriteriaBuilder UpdatedAfter(DateTime startDate)
        {
            m_criteria.Add(new DateItemFinderCriterion(UPDATE_FIELD_NAME, startDate, DateTime.MaxValue));
            return this;
        }

        public IItemFinderCriterion BuildAndCriteria()
        {
            return new AndCompositeItemFinderCriterion(m_criteria);
        }

        public IItemFinderCriterion BuildOrCriteria()
        {
            return new OrCompositeItemFinderCriterion(m_criteria);
        }
    }
}


