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
            var myFirstObject = Singleton.Instance();
            var mySecondObject = Singleton.Instance();
            var myThirdObject = Singleton.Instance();
            //var myFourthObject = new Singleton(); 

            if(myFirstObject == mySecondObject)
                Console.WriteLine("Nesneler aynı");

            if(mySecondObject != myThirdObject)
                Console.WriteLine("Nesneler aynı değil");

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
            Console.WriteLine("Nesne oluşturuluyor...");
        }

        public static Singleton Instance()
        {
            // Bu kod thread-safe bir kod değil
            if (_instance == null)
                _instance = new Singleton();

            return _instance;
        }
    }
}
