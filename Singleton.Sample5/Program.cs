using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Singleton.Sample5
{
    class Program
    {
        static void Main(string[] args)
        {
            // 0
            Console.WriteLine("Singleton Version 0");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            SingletonV0 v01 = SingletonV0.Instance();
            v01.ToString();
            SingletonV0 v02 = SingletonV0.Instance();
            v02.ToString();
            SingletonV0 v03 = SingletonV0.Instance();
            v03.ToString();
            
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());

            // 1
            Console.WriteLine("Singleton Version 1");
            sw.Restart();

            SingletonV1 v04 = SingletonV1.Instance;
            v04.ToString();
            SingletonV1 v05 = SingletonV1.Instance;
            v05.ToString();
            SingletonV1 v06 = SingletonV1.Instance;
            v06.ToString();

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());

            // 2
            Console.WriteLine("Singleton Version 2");
            sw.Restart();


            SingletonV2 v07 = SingletonV2.Instance;
            v07.ToString();
            SingletonV2 v08 = SingletonV2.Instance;
            v08.ToString();
            SingletonV2 v09 = SingletonV2.Instance;
            v09.ToString();

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());

            //3
            Console.WriteLine("Singleton Version 3");
            sw.Restart();


            SingletonV3 v10 = SingletonV3.Instance;
            v10.ToString();
            SingletonV3 v11 = SingletonV3.Instance;
            v11.ToString();
            SingletonV3 v12 = SingletonV3.Instance;
            v12.ToString();

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());

            Console.WriteLine("Singleton Version 4");
            sw.Restart();

            TestClass c = Singleton<TestClass>.UniqueInstanse;
            TestClass c1 = Singleton<TestClass>.UniqueInstanse;
            TestClass c3 = Singleton<TestClass>.UniqueInstanse;

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());


            Console.ReadLine();
        }
    }


    // Version 0
    class SingletonV0
    {
        private static SingletonV0 _instance = null;

        // yapıcı methodun erişim belirleyicisi 'protected' 
        // private da kullanabilirdir.
        protected SingletonV0()
        {

        }

        public static SingletonV0 Instance()
        {
            // Bu kod thread-safe bir kod değil
            if (_instance == null)
                _instance = new SingletonV0();

            return _instance;
        }

        public override string ToString()
        {
            return "Test0";
        }
    }

    public sealed class SingletonV1
    {
        static SingletonV1 instance = null;
        static readonly object padlock = new object();

        SingletonV1()
        {
        }

        public static SingletonV1 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonV1();
                    }
                    return instance;
                }
            }
        }

        public override string ToString()
        {
            return "Test1";
        }
    }

    public class SingletonV2
    {
        private SingletonV2()
        {
        }

        public static SingletonV2 Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly SingletonV2 instance = new SingletonV2();
        }

        public override string ToString()
        {
            return "Test2";
        }
    }

    class SingletonV3
         : SingletonBase<SingletonV3>
    {
        public string Something { get; set; }

        // !
        private SingletonV3()
        {
            
        }

        public override string ToString()
        {
            return "Test3";
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
            internal static readonly T instance= new T();
        }

        public static T UniqueInstanse 
        {
            get { return SingletonCreator.instance; }
        }

    }
    
}
