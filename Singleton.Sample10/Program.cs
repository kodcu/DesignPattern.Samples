using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample10
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileProcessor fileProcessor = DataProcessor.Instance;

            var paymentFile = @"C:\Payment\out\20141204_940_159856.xml";

            fileProcessor.Valid(paymentFile);
            fileProcessor.Parse(paymentFile);

            DataProcessor.Instance.Valid(paymentFile);
            DataProcessor.Instance.Parse(paymentFile);

            Console.ReadKey();
        }
    }


    public interface IFileProcessor
    {
        void Valid(string path);
        void Parse(string path);
    }

    public sealed class DataProcessor
        :IFileProcessor
    {
        static DataProcessor instance = null;
        static readonly object padlock = new object();

        DataProcessor()
        {
            Console.WriteLine("Nesne oluştu. !");
        }

        public static DataProcessor Instance
        {
            get
            {
                // multi-thread ortamlar için lock ile paylaşılan nesne aracılığıyla
                // kontrol ediliyor.
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataProcessor();
                    }
                    return instance;
                }
            }
        }

        public void Valid(string path)
        {
            Console.WriteLine("Dosyanın geçerliliği kontrol ediliyor...");
        }

        public void Parse(string path)
        {
            Console.WriteLine("Dosya ayrıştırılıyor...");
        }
    }
}
