using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiton.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t = Multiton<Test>.GetInstance("");
            Test t1 = Multiton<Test>.GetInstance("");
            Test t2 = Multiton<Test>.GetInstance("Test");

            if(t == t1)
                Console.WriteLine("Aynı nesneler");

            if (t2 != t1)
                Console.WriteLine("Farklı nesneler");

            Console.ReadKey();
        }
    }

    public class Test
    {

    }

    //public class Multiton<T> where T : new() // generic multition.
    //{
    //    private static readonly ConcurrentDictionary<object, T> _instances = new ConcurrentDictionary<object, T>();

    //    private Multiton() { }

    //    public static T GetInstance(object key)
    //    {
    //        return _instances.GetOrAdd(key, (k) => new T());
    //    }
    //}

    public sealed class Multiton<T> where T 
        : class, new()
    {
        private static readonly ConcurrentDictionary<object, Lazy<T>> _instances = new ConcurrentDictionary<object, Lazy<T>>();

        public static T GetInstance(object key)
        {
            Lazy<T> instance = _instances.GetOrAdd(key, k => new Lazy<T>(() => new T()));
            return instance.Value;
        }

        private Multiton()
        {
        }
    }
}
