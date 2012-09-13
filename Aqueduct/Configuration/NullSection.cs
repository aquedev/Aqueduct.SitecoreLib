using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aqueduct.Common;
using Aqueduct.Common.Context;

namespace Aqueduct.Configuration
{
    public class NullSection : Section
    {
        private static readonly Section _instance = new NullSection ();

        private NullSection ()
            : base ("")
        {
        }

        public override bool IsNull
        {
            get { return true; }
        }

        public override ApplicationMode Mode
        {
            get { return ApplicationMode.Disabled; }
            protected set { }
        }

        public override string Name
        {
            get { return String.Empty; }
            protected set { }
        }

        public override ReadOnlyCollection<string> Servers
        {
            get { return new ReadOnlyCollection<string> (new List<string> ()); }
            protected set { }
        }

        public static Section Instance
        {
            get { return _instance; }
        }

        public override bool IsActive (IContext context)
        {
            return false;
        }

        public override bool Equals (object obj)
        {
            return obj is NullSection;
        }

        public override int GetHashCode ()
        {
            return _instance.GetHashCode ();
        }
    }
}