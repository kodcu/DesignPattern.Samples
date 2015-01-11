using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiton.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiton mt = Multiton.GetInstance("");

            Multiton mt1 = Multiton.GetInstance("");

            Multiton mt2 = Multiton.GetInstance("Test");

            Multiton mt3 = Multiton.GetInstance("Test");


            if(mt == mt1)
                Console.WriteLine("Nesneler aynı");

            if (mt != mt2)
                Console.WriteLine("Nesneler farklı");

            Console.ReadKey();

        }
    }

    public sealed class Multiton
    {
        static Dictionary<string, Multiton> _instances = new Dictionary<string, Multiton>();
        // burada nesne koleksiyonumuzu tanımlıyoruz
        // nesneleri tanımlamak veya gruplamak için kullanacağımız key tipi 
        // string , int ya da istediğiniz herhangi bir veri tipi olabilir. 

        static object _lock = new object();

        private Multiton()
        {
            // private ctor
            Console.WriteLine("Nesne yaratıldı");
        }

        public static Multiton GetInstance(string key)
        {
            lock (_lock)
            {
                if (!_instances.ContainsKey(key))
                    _instances.Add(key, new Multiton());
            }
            return _instances[key];
        }

    }
}
