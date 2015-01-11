using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample8
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }

    class Facade
    {
        private SubSystemOne _one;
        private SubSystemTwo _two;
        private SubSystemThree _three;

        // private
        Facade()
        {
            _one = new SubSystemOne();
            _two = new SubSystemTwo();
            _three = new SubSystemThree();
        }

        static Facade() { }
        // private object
        static readonly Facade UniqueInstance = new Facade();

        // public static property
        public static Facade Instance 
        {
            get { return UniqueInstance; }
        }

        public void MethodA()
        {
            _one.MethodOne();
            _two.MethodTwo();
        }

        public void MethodB()
        {
            _two.MethodTwo();
            _three.MethodThree();
        }
    }

    class SubSystemOne
    {
        public void MethodOne()
        {
            Console.WriteLine(" SubSystemOne Method");
        }
    }

    class SubSystemTwo
    {
        public void MethodTwo()
        {
            Console.WriteLine(" SubSystemTwo Method");
        }
    }

    class SubSystemThree
    {
        public void MethodThree()
        {
            Console.WriteLine(" SubSystemThree Method");
        }
    }

}
