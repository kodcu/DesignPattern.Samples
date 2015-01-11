using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            string customerService = AppConfig.Instance.CustomerServiceAddress;
            string paymentFilePath = AppConfig.Instance.ForeignPaymentInFilePath;
            bool isProd = AppConfig.Instance.isProd;

            Console.WriteLine();
            Console.WriteLine(AppConfig.Instance.ForeignPaymentInFilePath);
            
            Console.WriteLine(customerService);
            Console.WriteLine(isProd);
            Console.WriteLine(paymentFilePath);


            Console.ReadLine();
        }
    }

    public class AppConfig
    {
        private AppConfig()
        {
            Console.WriteLine("nesne oluşturuldu.");
        }

        public static AppConfig Instance
        {
            get
            {
                // nesne yaratma işlemi nested tip üstleniyor
                return AppConfigCreator.instance;
            }
        }
        
        // nested bir tipin içerisindeki statik bir nesne asıl tipimizin nesnesini oluşturur
        // ve bu değişken aracılııyla nesneye erişiriz
        private class AppConfigCreator
        {
            static AppConfigCreator()
            {
            }

            internal static readonly AppConfig instance = new AppConfig();
        }

        #region functional properties

        public string CustomerServiceAddress
        {
            get { return "http://www.webservicex.net/stockquote.asmx"; }
        }

        public bool isProd
        {
            get { return false; }
        }

        public string ForeignPaymentInFilePath
        {
            get { return @"C:\PAYMENTS\FOREIGN\IN\"; }
        }

        #endregion
        
    }
}
