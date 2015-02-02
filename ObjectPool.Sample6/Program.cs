using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectPool.Sample6
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            // Create an opportunity for the user to cancel.
            Task.Run(() =>
            {
                if (Console.ReadKey().KeyChar == 'c' || Console.ReadKey().KeyChar == 'C')
                    cts.Cancel();
            });

            ObjectPool<MyClass> pool = new ObjectPool<MyClass>(() => new MyClass());

            // Create a high demand for MyClass objects.
            Parallel.For(0, 1000000, (i, loopState) =>
            {
                MyClass mc = pool.GetObject();
                Console.CursorLeft = 0;
                // This is the bottleneck in our application. All threads in this loop 
                // must serialize their access to the static Console class.
                Console.WriteLine("{0:####.####}", mc.GetValue(i));

                pool.PutObject(mc);
                if (cts.Token.IsCancellationRequested)
                    loopState.Stop();

            });
            Console.WriteLine("Press the Enter key to exit.");
            Console.ReadLine();
        }
    }

    public class ObjectPool<T>
    {
        private ConcurrentBag<T> objects;
        private Func<T> objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            if (objectGenerator == null) 
                throw new ArgumentNullException("objectGenerator");

            objects = new ConcurrentBag<T>();
            this.objectGenerator = objectGenerator;
        }

        public T GetObject()
        {
            T item;
            if (objects.TryTake(out item)) 
                return item;

            return objectGenerator();
        }

        public void PutObject(T item)
        {
            objects.Add(item);
        }
    }

    // A toy class that requires some resources to create. 
    // You can experiment here to measure the performance of the 
    // object pool vs. ordinary instantiation. 
    class MyClass
    {
        public int[] Nums { get; set; }
        public double GetValue(long i)
        {
            return Math.Sqrt(Nums[i]);
        }
        public MyClass()
        {
            Nums = new int[1000000];
            Random rand = new Random();
            for (int i = 0; i < Nums.Length; i++)
                Nums[i] = rand.Next();
        }
    }   

}
