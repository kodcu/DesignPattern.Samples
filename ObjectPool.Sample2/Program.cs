using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            BulletPool queuePool = new BulletPool();
            queuePool.Set();

            Bullet p = queuePool.Get();
            Bullet d = queuePool.Get();
            Bullet e = queuePool.Get();

            Console.ReadLine();
        }
    }

    public class Bullet
    {
        public int ColorCode { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public Bullet()
        {
            Console.WriteLine("Nesne oluştu");
        }
    }

    public interface IBulletPool
    {
        Bullet Get();
        void Set(Bullet bullet);
        void Set();
        void Clear();
    }

    // Enqueue () methodu listenin sonuna yeni item ekler - ADD
    // Dequeue () methodu listenin başındaki item verir ve bu item i siler - GET and DELETE
    // Queue koleksiyonu varsayılan olarak 32 kapasitesi vardır.
    // FIFO - First In First Out - prensipine göre çalışır.
    public class BulletPool
        : IBulletPool
    {
        private readonly Queue queue = null;
        public int Capacity { get; set; } 

        public BulletPool()
        {
            if(this.Capacity.Equals(null))
                this.Capacity = 32;

            this.queue = new Queue(Capacity);
        }

        public Bullet Get()
        {
            return (Bullet)this.queue.Dequeue();
        }

        public void Set()
        {
            Bullet b = new Bullet();
            this.queue.Enqueue(b);
        }

        public void Set(Bullet bullet)
        {
            this.queue.Enqueue(bullet);
        }

        public void Clear()
        {
            this.queue.Clear();
        }
    }
}
