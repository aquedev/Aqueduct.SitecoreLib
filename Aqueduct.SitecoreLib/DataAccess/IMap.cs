using System;
using System.Collections.Generic;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public interface IMap
    {
        bool SkipTemplateChecking { get; }
        string TemplatePath { get; }
        IList<MapEntry> Mappings { get; }
        Type MappedEntityType { get; }
    }
}