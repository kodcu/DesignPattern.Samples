using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample7
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    /// <summary>
    /// A simple lightweight object pool for fast and simple object reuse.
    /// Fast lightweight thread-safe object pool for objects that are expensive to create or could efficiently be reused.
    /// Note: this nuget package contains c# source code and depends on System.Collections.Concurrent introduced in .Net 4.0.
    /// </summary>
    public class ObjectPool<T> where T : class
    {
        static ConcurrentStack<T> objectBag = new ConcurrentStack<T>();

        public static int MaxCapacity = 10;
        public static Func<T> DefaultInstanceFactory = null;
        public static Action<T> DefaultInstanceDispose = null;
        
        public static T Get(Func<T> instanceFactory)
        {
            T item;
            if (!objectBag.TryPop(out item))
            {
                return instanceFactory();
            }
            return item;
        }

        public static T Get()
        {
            T item;
            if (!objectBag.TryPop(out item))
            {
                if (DefaultInstanceFactory == null)
                    return null;
                return DefaultInstanceFactory();
            }
            return item;
        }

        public static void Put(T item)
        {
            // add to pool if it is not full
            if (objectBag.Count < MaxCapacity)
            {
                objectBag.Push(item);
            }
            else if (DefaultInstanceDispose != null)
            {
                DefaultInstanceDispose(item);
            }
        }

        public static void Clear()
        {
            if (DefaultInstanceDispose != null)
            {
                T item;
                while (objectBag.TryPop(out item))
                {
                    DefaultInstanceDispose(item);
                }
            }
            objectBag.Clear();
        }

        public static int Count
        {
            get { return objectBag.Count; }
        }
    }
}
