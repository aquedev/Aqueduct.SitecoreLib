using System;
using System.Collections.Generic;
using System.Linq;

namespace Aqueduct.Configuration
{
    public class SectionList : List<Section>
    {
        public Section this [string name]
        {
            get
            {
                return this.FirstOrDefault (section =>
                                            section.Name.Equals (name, StringComparison.CurrentCultureIgnoreCase))
                       ?? NullSection.Instance;
            }
        }

        public Section Global
        {
            get { return this.FirstOrDefault (section => section.IsGlobal) ?? NullSection.Instance; }
        }
    }
}