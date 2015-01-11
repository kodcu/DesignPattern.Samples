using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {                        

            Console.ReadLine();
        }
    }



    public class TestClass
    {
        public override string ToString()
        {
            return "Test4";
        }
    }

    public class Singleton<T>
        where T : class,new()
    {
        Singleton()
        {

        }

        class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly T instance = new T();
        }

        public static T UniqueInstanse
        {
            get { return SingletonCreator.instance; }
        }
    }




    // başka bir örnek kod

    class SingletonSample
         : SingletonBase<SingletonSample>
    {
        public string Something { get; set; }

        // !
        private SingletonSample()
        {
            Console.WriteLine("Test");
        }

    }

    public abstract class SingletonBase<T>
        where T : class
    {
        private static readonly Lazy<T> _Instance = new Lazy<T>(() => CreateInstanceOfT());

        public static T Instance { get { return _Instance.Value; } }

        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
