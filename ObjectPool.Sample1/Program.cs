using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    /* The PooledObject class is the type that is expensive or slow to instantiate, 
     * or that has limited availability, so is to be held in the object pool. */
    /* 
        PooledObject sınıfı oluşturulması yavaş , masraflı veya sınırlı bir kullanıma sahip nesnedir
     *  Object pool içerisinde tutulan bir nesnedir.
     */
    public class PooledObject
    {
        DateTime _createdAt = DateTime.Now;

        public DateTime CreatedAt
        {
            get { return _createdAt; }
        }

        public string TempData { get; set; }
    }

    /*  The Pool class is the most important class in the object pool design pattern. It controls access to the
            pooled objects, maintaining a list of available objects and a collection of objects that have already been
            requested from the pool and are still in use. The pool also ensures that objects that have been released
            are returned to a suitable state, ready for the next time they are requested. */
    public static class Pool
    {
        private static List<PooledObject> _available = new List<PooledObject>();
        private static List<PooledObject> _inUse = new List<PooledObject>();

        public static PooledObject GetObject()
        {
            lock (_available)
            {
                if (_available.Count != 0)
                {
                    PooledObject po = _available[0];
                    _inUse.Add(po);
                    _available.RemoveAt(0);
                    return po;
                }
                else
                {
                    PooledObject po = new PooledObject();
                    _inUse.Add(po);
                    return po;
                }
            }
        }

        public static void ReleaseObject(PooledObject po)
        {
            CleanUp(po);

            lock (_available)
            {
                _available.Add(po);
                _inUse.Remove(po);
            }
        }

        private static void CleanUp(PooledObject po)
        {
            po.TempData = null;
        }
    }
}
