using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSA.Objects
{
    public class AppGroup : System.Collections.IList
    {
        public String Title { get; set; }

        protected List<App> Contents { get; set; }

        public AppGroup()
        {
            Contents = new List<App>();
        }

        public int Add(object value)
        {
            Contents.Add((App)value);
            return Contents.IndexOf((App)value);
        }

        public void Clear()
        {
            Contents.Clear();
        }

        public bool Contains(object value)
        {
           return Contents.Contains((App)value);
        }

        public int IndexOf(object value)
        {
            return Contents.IndexOf((App)value);
        }

        public void Insert(int index, object value)
        {
            Contents.Insert(index,(App)value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(object value)
        {
            Contents.Remove((App)value);
        }

        public void RemoveAt(int index)
        {
            Contents.RemoveAt(index);
        }

        public object this[int index]
        {
            get
            {
                return Contents[index];
            }
            set
            {
                Contents[index] = (App)value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            Contents.CopyTo((App[])array, index);
        }

        public int Count
        {
            get { return Contents.Count; }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return Contents.GetEnumerator();
        }
    }
}
