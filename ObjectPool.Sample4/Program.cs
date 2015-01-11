using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample4
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectPoolObject<Person> personPoolObject = new ObjectPoolObject<Person>(10);
            Person a = personPoolObject.Get();
            Person a1 = personPoolObject.Get();

            Console.ReadLine();
        }
    }

    public class Person
    {
        public Person()
        {
            Console.WriteLine("Nesne oluştu");
        }
    }

    public sealed class ObjectPoolObject<T> where T : new()
    {
        private readonly int _poolSize;
        private readonly object _threadLocker;
        private readonly Queue<T> _poolManager;

        public ObjectPoolObject(int expectedPoolSize)
        {
            if (expectedPoolSize <= 0)
                throw new ArgumentOutOfRangeException("expectedPoolSize", "Pool size can't be null or zero or less than zero");

            _poolManager = new Queue<T>();
            _threadLocker = new object();
            _poolSize = expectedPoolSize;
        }

        public T Get()
        {
            lock (_threadLocker)
            {
                if (_poolManager.Count > 0)
                {
                    return _poolManager.Dequeue();
                }
                else
                {
                    return new T();
                }
            }
        }

        public int Count()
        {
            return _poolManager.Count;
        }

        public void Put(T obj)
        {
            lock (_threadLocker)
            {
                if (_poolManager.Count < _poolSize)
                    _poolManager.Enqueue(obj);
            }
        }
    }
}
