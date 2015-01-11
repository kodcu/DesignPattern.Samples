using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample4
{
    // source : http://www.yoda.arachsys.com/csharp/singleton.html

    class Program
    {
        static void Main(string[] args)
        {


            Console.ReadLine();
        }
    }

    // First version - not thread-safe
    // Bad code! Do not use!
    public sealed class Singleton
    {
        static Singleton instance = null;

        Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }

    // Second version - simple thread-safety
    public sealed class SingletonV2
    {
        static SingletonV2 instance = null;
        static readonly object padlock = new object();

        SingletonV2()
        {
        }

        public static SingletonV2 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonV2();
                    }
                    return instance;
                }
            }
        }
    }

    // Third version - attempted thread-safety using double-check locking
    // Bad code! Do not use!
    public sealed class SingletonV3
    {
        static SingletonV3 instance = null;
        static readonly object padlock = new object();

        SingletonV3()
        {
        }

        public static SingletonV3 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonV3();
                        }
                    }
                }
                return instance;
            }
        }
    }

    // Fourth version - not quite as lazy, but thread-safe without using locks
    public sealed class SingletonV4
    {
        static readonly SingletonV4 instance = new SingletonV4();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonV4()
        {
        }

        SingletonV4()
        {
        }

        public static SingletonV4 Instance
        {
            get
            {
                return instance;
            }
        }
    }

    // Fifth version - fully lazy instantiation
    public sealed class SingletonV5
    {
        SingletonV5()
        {
        }

        public static SingletonV5 Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly SingletonV5 instance = new SingletonV5();
        }
    }
}
