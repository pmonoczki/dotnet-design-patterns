using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P09_Iterator
{
    class OverIndexableList<T> : IEnumerable<T>
    {
        T[] items = new T[0];

        public T this[int index]
        {
            get 
            {
                if (index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else if (index >= items.Length)
                {
                    T[] temp = items;
                    items = new T[index + 1];
                    temp.CopyTo(items, 0);
                }
                return items[index];
            }
            set 
            {
                if (index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else if (index >= items.Length)
                {
                    T[] temp = items;
                    items = new T[index + 1];
                    temp.CopyTo(items, 0);
                }
                items[index] = value;
            }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            //return new OverIndexableListEnumerator<T>(this);
            for (int i = 0; i < items.Length; i++)
            {
                yield return items[i];
            }

        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        public class OverIndexableListEnumerator<T> : IEnumerator<T>
        {
            OverIndexableList<T> list = null;

            public OverIndexableListEnumerator(
                OverIndexableList<T> list)
            {
                this.list = list;
            }

            int currentIndex = -1;

            #region IEnumerator<T> Members

            public T Current
            {
                get 
                {
                    return list.items[currentIndex];
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.list = null;
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return list.items[currentIndex]; }
            }

            public bool MoveNext()
            {
                currentIndex++;
                return currentIndex < list.items.Length;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            #endregion
        }
    }
}
