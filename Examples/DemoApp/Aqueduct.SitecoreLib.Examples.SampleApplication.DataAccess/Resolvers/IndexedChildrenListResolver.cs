using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    public class IndexedChildrenListResolver<TChild> : ChildrenListResolver<TChild> where TChild : ISitecoreDomainEntity
    {
        private readonly Expression<Func<TChild, int>> m_expression;

        public IndexedChildrenListResolver(Expression<Func<TChild, int>> expression)
        {
            m_expression = expression;
        }

        protected override void ProcessChildren(Item item, IMap map, ReadOnlyRepository repository, IList list)
        {
            for (var i = 0; i < item.Children.Count; i++)
            {
                var child = item.Children[i];
                var entity = AddItem(child, map, repository, list);

                var prop = (PropertyInfo)((MemberExpression)m_expression.Body).Member;
                prop.SetValue(entity, i, null);
            }
        }

    }
}
