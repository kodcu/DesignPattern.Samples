using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Sample1
{
    //http://msdn.microsoft.com/en-us/library/orm-9780596527730-01-05.aspx

    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }

    [Serializable()]
    public abstract class IPrototype<T>
    {
        // Shallow Copy
        public T Clone()
        {
            return (T)this.MemberwiseClone();
        }

        // Deep Copy
        public T DeepCopy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            T copy = (T)formatter.Deserialize(stream);
            stream.Close();

            return copy;
        }
    }

    class DeeperData
    {
        public string Data { get; set; }

        public DeeperData(string s)
        {
            this.Data = s;
        }

        public override string ToString()
        {
            return Data;
        }
    }

    class Prototype :
        IPrototype<Prototype>
    {
        public string Country { get; set; }
        public string Capital { get; set; }
        public DeeperData Language { get; set; }


        public Prototype(string country, string capital, string language)
        {
            this.Country = country;
            this.Capital = capital;
            this.Language = new DeeperData(language);
        }

        public override string ToString()
        {
            return Country + "\t\t" + Capital + "\t\t->" + Language;
        }
    }

    class PrototypeManager
        : IPrototype<Prototype>
    {
        public Dictionary<string, Prototype> prototypes = new Dictionary<string, Prototype> 
        {
            {"Germany",new Prototype("Germany","Berlin","German")},
            {"Germany",new Prototype("Italy","Rome","Italian")},
            {"Germany",new Prototype("Australia","Canberra","English")}
        };

    }
}
