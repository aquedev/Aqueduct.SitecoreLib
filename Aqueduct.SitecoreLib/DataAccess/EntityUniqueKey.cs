using System;

namespace Aqueduct.SitecoreLib.DataAccess
{
    [Serializable]
    public class EntityUniqueKey : TwinKey<Guid, Type>
    {
        public EntityUniqueKey(Guid id, Type type)
            : base(id, type)
        {
        }
    }

    [Serializable]
    public class TwinKey<T1, T2> 
    {
        private readonly T1 m_subKey1;
        private readonly T2 m_subKey2 ; 

        public TwinKey (T1 subKey1, T2 subKey2)
        {
            m_subKey1 = subKey1;
            m_subKey2 = subKey2;
        }

        public override int GetHashCode()
        {
            return m_subKey1.GetHashCode() & m_subKey2.GetHashCode ();
        }

        public override bool Equals(object obj)
        {
            var otherKey = obj as TwinKey<T1, T2>;
            if (otherKey == null)
                return false;
            return m_subKey1.Equals(otherKey.m_subKey1) && m_subKey2.Equals(otherKey.m_subKey2);
        }
    }
}