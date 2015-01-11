using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SM.Instance.Get("WarningToUserPassword"));
            Console.WriteLine(SM.Instance.Get("WarningToUserPassword"));
            Console.WriteLine(SM.Instance.Get("WarningToUserPassword"));
            
            Console.WriteLine("");

            Console.WriteLine(SM.Instance.Get("CompletedToProcess","Ekran işlemi"));
            Console.WriteLine(SM.Instance.Get("CompletedToProcess", "Dosya kopyalama"));
            Console.WriteLine(SM.Instance.Get("CompletedToProcess", "Tarama"));

            Console.ReadLine();
        }
    }


    public interface IStringManager
    {
        string Get(string key);
        string Get(string key, params string[] prms);
    }

    public abstract class BaseStringManager
        : IStringManager
    {
        public abstract string Get(string key);
        public abstract string Get(string key, params string[] prms);
    }

    public class SM
        : BaseStringManager
    {
        private readonly Dictionary<string, string> values = new Dictionary<string, string>();

        private SM()
        {
            values.Add("WarningToUserPassword", "Kullanıcı şifresi hatalı");
            values.Add("CompletedToProcess", "{0} tamamladı");
            Console.WriteLine("Created");
        }

        public static SM Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly SM instance = new SM();
        }

        #region functional methods

        public override string Get(string key)
        {
            // normalde veritabanından bu keye karşılık gelen text metni getirir.
            return values[key];
        }

        public override string Get(string key, params string[] prms)
        {
            return String.Format(Get(key), prms);
        }

        #endregion

    }

}
