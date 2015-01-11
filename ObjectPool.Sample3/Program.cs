using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            QueuedPool<Person> queuePool = new QueuedPool<Person>();
            queuePool.Store(new Person());

            Person p = queuePool.Fetch();

            Person d = queuePool.Fetch();

            Console.ReadKey();
        }
    }

    public class Person
    {
        public Person()
        {
            Console.WriteLine("Nesne oluştu");
        }
    }

    public interface IStore<T>
    {
        T Fetch();
        void Store(T obj);
        int Count { get; }
        void ClearAll();
    }

    public class QueuedPool<T>
        : Queue<T>, IStore<T>
    {
        public T Fetch()
        {
            return (T)Dequeue();
        }

        public void Store(T obj)
        {
            Enqueue(obj);
        }

        public void ClearAll()
        {
            base.Clear();
        }
    }
}
