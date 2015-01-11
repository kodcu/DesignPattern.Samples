using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            QueuedPool queuePool = new QueuedPool();
            queuePool.Store(new Person());

            Person p = queuePool.Fetch();
            Person d = queuePool.Fetch();
            Person e = queuePool.Fetch();

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

    // Enqueue () methodu son uygun olan bölüme yeni nesne ekler
    // Dequeue () methodu kuyruktaki en son nesneyi siler ve bu nesneyi geri döner.
    public class QueuedPool
        : Queue
    {
        public Person Fetch()
        {
            return (Person)Dequeue();
        }

        public void Store(Person person)
        {
            Enqueue(person);
        }

        public void ClearAll()
        {
            base.Clear();
        }
    }
}
