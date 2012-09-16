using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Configuration.DSL;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Business.StructureMap
{
    public class InterfacesForClassRegistry<T> : Registry
    {
        public InterfacesForClassRegistry(bool singleton)
        {
            foreach (Type @interface in GetTopLevelInterfaces(typeof(T)))
            {
                if (singleton)
                    For(@interface).Singleton().Use(typeof(T));
                else
                    For(@interface).Use(typeof(T));
            }
        }

        private static IEnumerable<Type> GetTopLevelInterfaces(Type t)
        {
            var allInterfaces = t.GetInterfaces();
            var selection = allInterfaces
                .Where(x => !allInterfaces.Any(y => y.GetInterfaces().Contains(x)))
                .Except(t.BaseType.GetInterfaces());
            return selection.ToArray();
        }
    }
}