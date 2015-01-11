using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample9
{
    class Program
    {
        static void Main(string[] args)
        {
            // örnek kodun kullanımı

            ILogger logger = Logger.Instance;
            logger.Log(LogType.Error, "Test Error");
            logger.Log(LogType.Info, "İşlem tamamlandı");

            Logger.Instance.Log(LogType.Exception, "kodunuzu kontrol ediniz");


            Console.ReadLine();
        }
    }

    public enum LogType { Exception , Info , Error , Fatal }

    public interface ILogger
    {
        void Log(LogType type, string msg);
    }

    public sealed class Logger
        :ILogger
    {
        // Generic Lazy sınıfına kendi tipimizi veriyoruz
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());

        // lazy deişkeni üzerinden sınıfın instance değerini dönüyoruz
        public static Logger Instance { get { return lazy.Value; } }

        // private veya protected yapıcı method
        private Logger()
        {
            Console.WriteLine("yapıcı method çalıştı");
        }

        #region functional methods

        public void Log(LogType type, string msg)
        {
            Console.WriteLine(type.ToString() + " - " + msg);
        }

        #endregion
        
    }
}
