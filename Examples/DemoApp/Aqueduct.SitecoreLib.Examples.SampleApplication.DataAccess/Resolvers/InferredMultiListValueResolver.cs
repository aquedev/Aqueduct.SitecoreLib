using System;
using System.Collections;
using Aqueduct.Diagnostics;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.DataAccess.ValueResolvers;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    public class InferredMultiListValueResolver<T> : MultiListValueResolver<T>
       where T : ISitecoreDomainEntity
    {
        private readonly ReadOnlyRepository m_repository;

        public InferredMultiListValueResolver(string fieldName)
            : base(fieldName)
        {
            m_repository = new ReadOnlyRepository("web");
        }

        protected override void GetEntity(string id, IList list)
        {
            try
            {
                //Gets entity using an inferred map from the template name.
                var entity = m_repository.Get(new Guid(id));

                if (entity is T)
                    list.Add(entity);
            }
            catch (Exception ex)
            {
                AppLogger.LogError(string.Format("Error mapping item with ID {0} to type {1}", id, typeof(T)), ex);
            }
        }
    }
}
