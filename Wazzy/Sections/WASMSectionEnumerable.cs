using System.Collections;
using System.Collections.Generic;

namespace Wazzy.Sections
{
    public abstract class WASMSectionEnumerable<T> : WASMSection, IList<T>
    {
        protected List<T> Subsections { get; }

        protected WASMSectionEnumerable(WASMSectionId id)
            : base(id)
        {
            Subsections = new List<T>();
        }

        protected virtual void SubsectionsCleared()
        { }
        protected virtual void SubsectionAdded(T subsection)
        { }
        protected virtual void SubsectionRemoved(T subsection)
        { }

        #region IList<T> Implementation
        public T this[int index]
        {
            get => Subsections[index];
            set
            {
                Subsections[index] = value;
                SubsectionRemoved(value);
            }
        }
        public int Count => Subsections.Count;
        bool ICollection<T>.IsReadOnly => false;

        public void RemoveAt(int index)
        {
            T subsection = Subsections[index];
            Subsections.RemoveAt(index);
            SubsectionRemoved(subsection);
        }
        public void Insert(int index, T item)
        {
            Subsections.Insert(index, item);
            SubsectionAdded(item);
        }
        public int IndexOf(T item) => Subsections.IndexOf(item);

        public void Clear()
        {
            Subsections.Clear();
            SubsectionsCleared();
        }
        public void Add(T item)
        {
            Subsections.Add(item);
            SubsectionAdded(item);
        }
        public bool Remove(T item)
        {
            bool removed = Subsections.Remove(item);
            if (removed)
            {
                SubsectionRemoved(item);
            }
            return removed;
        }
        public bool Contains(T item) => Subsections.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => Subsections.CopyTo(array, arrayIndex);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Subsections).GetEnumerator();
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Subsections).GetEnumerator();
        #endregion
    }
}