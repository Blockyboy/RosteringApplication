using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosteringApplication.CustomClasses
{
    public class ObservableSet<T> :
    ISet<T>,
    INotifyCollectionChanged,
    INotifyPropertyChanged
    {
        private readonly HashSet<T> _set;

        public ObservableSet()
        {
            _set = new HashSet<T>();
        }

        public ObservableSet(IEqualityComparer<T>? comparer)
        {
            _set = new HashSet<T>(comparer);
        }

        public int Count => _set.Count;
        public bool IsReadOnly => false;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Add(T item)
        {
            if (!_set.Add(item))
                return false;

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add, item));

            return true;
        }

        void ICollection<T>.Add(T item) => Add(item);

        public bool Remove(T item)
        {
            if (!_set.Remove(item))
                return false;

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove, item));

            return true;
        }

        public void Clear()
        {
            if (_set.Count == 0)
                return;

            _set.Clear();

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item) => _set.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) =>
            _set.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // ISet<T>
        public void ExceptWith(IEnumerable<T> other) => Mutate(s => s.ExceptWith(other));
        public void IntersectWith(IEnumerable<T> other) => Mutate(s => s.IntersectWith(other));
        public void SymmetricExceptWith(IEnumerable<T> other) => Mutate(s => s.SymmetricExceptWith(other));
        public void UnionWith(IEnumerable<T> other) => Mutate(s => s.UnionWith(other));

        public bool IsProperSubsetOf(IEnumerable<T> other) => _set.IsProperSubsetOf(other);
        public bool IsProperSupersetOf(IEnumerable<T> other) => _set.IsProperSupersetOf(other);
        public bool IsSubsetOf(IEnumerable<T> other) => _set.IsSubsetOf(other);
        public bool IsSupersetOf(IEnumerable<T> other) => _set.IsSupersetOf(other);
        public bool Overlaps(IEnumerable<T> other) => _set.Overlaps(other);
        public bool SetEquals(IEnumerable<T> other) => _set.SetEquals(other);

        private void Mutate(Action<HashSet<T>> action)
        {
            var old = _set.ToHashSet();
            action(_set);

            if (!old.SetEquals(_set))
            {
                OnCollectionChanged(
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Reset));
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(Count)));
        }
    }
}
