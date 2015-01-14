using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Sample6
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }

    interface IFactory<Brand>
        where Brand : IBrand
    {
        IBag CreateBag();
        IShoes CreateShoes();
    }

    // Conctete Factories (both in the same one)
    class Factory<Brand> : IFactory<Brand>
        where Brand : IBrand, new()
    {
        public IBag CreateBag()
        {
            return new Bag<Brand>();
        }

        public IShoes CreateShoes()
        {
            return new Shoes<Brand>();
        }
    }

    // Product 1
    interface IBag
    {
        string Material { get; }
    }

    // Product 2
    interface IShoes
    {
        int Price { get; }
    }

    // Concrete Product 1
    class Bag<Brand> : IBag
        where Brand : IBrand, new()
    {
        private Brand myBrand;
        public Bag()
        {
            myBrand = new Brand();
        }

        public string Material { get { return myBrand.Material; } }
    }

    // Concrete Product 2
    class Shoes<Brand> : IShoes
        where Brand : IBrand, new()
    {
        private Brand myBrand;
        public Shoes()
        {
            myBrand = new Brand();
        }

        public int Price { get { return myBrand.Price; } }
    }

    // 
    interface IBrand
    {
        int Price { get; }
        string Material { get; }
    }

    class Gucci : IBrand
    {
        public int Price { get { return 1000; } }
        public string Material { get { return "Crocodile skin"; } }
    }

    class Poochy : IBrand
    {
        public int Price { get { return new Gucci().Price / 3; } }
        public string Material { get { return "Plastic"; } }
    }

    class Groundcover : IBrand
    {
        public int Price { get { return 2000; } }
        public string Material { get { return "South african leather"; } }
    }
}
