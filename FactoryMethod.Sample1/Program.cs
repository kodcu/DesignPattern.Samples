using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory s = new Factory();
            IOperation opr = s.FactoryMethod(OperationType.CategoryOperation);
            Console.WriteLine(opr.GetAllList());


            IOperation opr1 = OperationFactory<CategoryOperation>.FactoryMethod();
            Console.WriteLine(opr1.GetAllList());

            Console.ReadLine();
        }
    }

    interface IOperation
    {
        string GetAllList();
    }

    class LinkOperation
        : IOperation
    {
        public string GetAllList()
        {
            return "Link";
        }
    }

    class PostOperation
        : IOperation
    {
        public string GetAllList()
        {
            return "Post";
        }
    }

    class CategoryOperation
        : IOperation
    {
        public string GetAllList()
        {
            return "Category";
        }
    }

    enum OperationType
    {
        PostOperation,
        CategoryOperation,
        LinkOperation
    }

    class Factory
    {
        public IOperation FactoryMethod(OperationType type)
        {
            IOperation opr = null;

            switch (type)
            {
                case OperationType.PostOperation:
                    opr = new PostOperation();
                    break;
                case OperationType.CategoryOperation:
                    opr = new CategoryOperation();
                    break;
                default:
                    break;
            }
            return opr;
        }

    }



    class OperationFactory<T>
        where T : IOperation, new()
    {
        private static IOperation opr = null;

        public static IOperation FactoryMethod()
        {
            return opr = new T();
        }
    }


    // başka bir örnek tasarım
    // Generic Factory 
    public class GlobalFactory
    {
        public T Create<T>() where T : class
        {
            var type = typeof(T);
            return Activator.CreateInstance(type) as T;
        }
    }
}
