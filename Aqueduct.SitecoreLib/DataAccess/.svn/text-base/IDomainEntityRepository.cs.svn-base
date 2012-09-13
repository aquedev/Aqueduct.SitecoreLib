using System;
using System.Collections.Generic;
using Aqueduct.Domain;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public interface IDomainEntityRepository
    {
        ISitecoreDomainEntity GetEntity(Guid id);
        IList<ISitecoreDomainEntity> GetEntities(IEnumerable<Guid> ids, IMap map);
    }
}
