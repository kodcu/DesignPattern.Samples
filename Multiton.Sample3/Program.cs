using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiton.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "John Doe";

            Card expected = Rolodex.Open(key);
            expected.Information = "john.doe@example.com";

            Card actual = Rolodex.Open(key);
            Console.WriteLine(actual.Key + actual.Information);

            Card actual1 = Rolodex.Open(key);
            Console.WriteLine(actual1.Key + actual1.Information);

            Card actual2 = Rolodex.Open(key);
            Console.WriteLine(actual2.Key + actual2.Information);

            Console.ReadLine();
        }
    }

    public sealed class Card
    {
        internal Card(string key)
        {
            this.Key = key;
        }

        public string Information { get; set; }
        public string Key { get; set; }

    }

    public sealed class Rolodex
    {
        private static Rolodex _rolodex = new Rolodex();

        private Rolodex()
        {
            this.Cards = new Collection<Card>();
        }

        private Collection<Card> Cards { get; set; }

        public static Card Open(string key)
        {
            Card result = null;

            lock (_rolodex)
            {
                result = _rolodex.Cards
                    .Where(x => string.Equals(x.Key, key, StringComparison.Ordinal))
                    .FirstOrDefault();

                if (null == result)
                {
                    result = new Card(key);
                    _rolodex.Cards.Add(result);
                }
            }

            return result;
        }
    }
}
