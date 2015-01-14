using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Sample5
{
    class Program
    {
        static void Main(string[] args)
        {
            ProviderFactory<SqlDataProvider> providerFactory = new ProviderFactory<SqlDataProvider>();

            Object data = providerFactory.GetProvider().Provide();

            Console.ReadLine();
        }
    }

    interface IDataProvider  
    {
        Object Provide();
    }

    public class SqlDataProvider
        : IDataProvider
    {

        public object Provide()
        {
            Console.WriteLine("SQL Provider");
            return null;
        }
    }

    public class OracleDataProvider
        : IDataProvider
    {

        public object Provide()
        {
            Console.WriteLine("ORACLE Provider");
            return null;
        }
    }

    public class ProviderFactory<T> 
        where T : IDataProvider, new()
    {
        internal ProviderFactory()
        {
        }

        internal T GetProvider()
        {
            // IDataProvider i saglayan siniflarin
            // parametresiz constructor u bulunmasi gerekmekte.
            return new T();
        }
    }   
}
