using System;
using System.Collections.Generic;
using System.Collections;
using Aqueduct.Utils;

namespace Aqueduct.Common
{
    public class LazyList<T> : IList<T>, IList
    {
        object m_lock = new object();

        bool m_actionExecuted;
        private Func<IEnumerable> m_loadAction;

        private List<T> m_internalList;
        private List<T> InternalList
        {
            get
            {
                CheckInitialized();

                return m_internalList;
            }
        }

        private IList InternalIList
        {
            get { return InternalList as IList; }
        }

        private void CheckInitialized()
        {
            if (m_actionExecuted == false)
                lock (m_lock)
                {
                    if (m_actionExecuted != false)
                        return;

                    m_internalList = new List<T>();
                    foreach (object item in m_loadAction.Invoke())
                    {
                        m_internalList.Add((T)item);
                    }
                    m_actionExecuted = true;
                }
        }

        public LazyList(Func<IEnumerable<T>> loadAction) : this(() => (IEnumerable)loadAction.Invoke())
        {
            
        }

        public LazyList(Func<IEnumerable> loadAction)
        {
            Guard.ParameterNotNull(loadAction, "Load action");
            m_loadAction = loadAction;
        }

        public int IndexOf(T item)
        {
            return InternalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            InternalList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            RemoveAt(index);
        }

        public T this[int index]
        {
            get { return InternalList[index]; }
            set { InternalList[index] = value; }
        }

        public void Add(T item)
        {
            InternalList.Add(item);
        }

        public void Clear()
        {
            InternalList.Clear();
        }

        public bool Contains(T item)
        {
            return InternalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InternalList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return InternalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return InternalList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InternalList.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)InternalList).GetEnumerator();
        }


        #region IList Members

        public int Add(object value)
        {
            return InternalIList.Add(value);
        }

        public bool Contains(object value)
        {
            return InternalIList.Contains(value);
        }

        public int IndexOf(object value)
        {
            return InternalIList.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            InternalIList.Insert(index, value);
        }

        public bool IsFixedSize
        {
            get { return InternalIList.IsFixedSize; }
        }

        public void Remove(object value)
        {
            InternalIList.Remove(value);
        }

        object IList.this[int index]
        {
            get
            {
                return InternalIList[index];
            }
            set
            {
                InternalIList[index] = value;
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            InternalIList.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get { return InternalIList.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return InternalIList.SyncRoot; }
        }

        #endregion
    }
}
