using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample5
{
    class Program
    {
        static void Main(string[] args)
        {
            object obj1 = new object();
            object obj2 = new object();

            PoolManager poolManager = PoolManager.Instance;
            poolManager.AddObject(obj1);
            poolManager.AddObject(obj2);

            Console.WriteLine(poolManager.CurrentObjectsInPool.ToString());
            poolManager.ReleaseObject(obj1);
            Console.WriteLine(poolManager.CurrentObjectsInPool.ToString());

            object obj = null;
            for (; ; )
            {
                obj = poolManager.ReleaseObject();
                if (obj != null)
                    Console.WriteLine(obj.GetHashCode().ToString());
                else
                {
                    Console.WriteLine("No more objects in the pool");
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
