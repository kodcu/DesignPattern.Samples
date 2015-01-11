using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            var myObject = Singleton.Instance();

            Console.WriteLine(myObject.DoSomething());
            Console.WriteLine(myObject.DoSomething());
            Console.WriteLine(myObject.DoSomething());

            Console.ReadLine();
        }
    }

    class Singleton
    {
        private static Singleton _instance = null;

        // yapıcı methodun erişim belirleyicisi 'protected' 
        // private da kullanabilirdir.
        protected Singleton()
        {

        }

        public static Singleton Instance()
        {
            // Bu kod thread-safe bir kod değil
            if (_instance == null)
                _instance = new Singleton();

            return _instance;
        }

        public string DoSomething()
        {
            return _instance.GetHashCode().ToString();
        }
    }
}
