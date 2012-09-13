using System;

namespace Aqueduct.Common.Context
{
    public class NullContext : BaseContext
    {
        private static IContext _instance = new NullContext();
        public static IContext Instance
        {
            get { return _instance; }
        }

        protected NullContext()
            : base(ApplicationMode.Auto, String.Empty)
        {
           
        }
    }
}
