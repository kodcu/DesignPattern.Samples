using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool.Sample5
{
    /// <summary>
    /// A class to manage objects in a pool. 
    ///The class is sealed to prevent further inheritence
    /// and is based on the Singleton Design.
    /// </summary>
    public sealed class PoolManager
    {
        private Queue poolQueue = new Queue();
        private Hashtable objPool = new Hashtable();

        private static readonly object objLock = new object();
        private static PoolManager poolInstance = null;

        private const int POOL_SIZE = 10;
        private int objCount = 0;

        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private PoolManager()
        {

        }

        /// <summary>
        /// Static constructor that gets 
        ///called only once during the application's lifetime.
        /// </summary>
        static PoolManager()
        {
            poolInstance = new PoolManager();
        }

        /// <summary>
        /// Static property to retrieve the instance of the Pool Manager
        /// </summary>
        public static PoolManager Instance
        {
            get
            {
                if (poolInstance != null)
                {
                    return poolInstance;
                }

                return null;
            }
        }

        /// <summary>
        /// Creates objects and adds them in the pool
        /// </summary>
        /// <param name="obj">The object type</param>
        public void CreateObjects(object obj)
        {
            object _obj = obj;
            objCount = 0;
            poolQueue.Clear();
            objPool.Clear();

            for (int objCtr = 0; objCtr < POOL_SIZE; objCtr++)
            {
                _obj = new object();

                lock (objLock)
                {
                    objPool.Add(_obj.GetHashCode(), _obj);
                    poolQueue.Enqueue(_obj);
                    objCount++;
                }
            }
        }

        /// <summary>
        /// Adds an object to the pool
        /// </summary>
        /// <param name="obj">Object to be added</param>
        /// <returns>True if success, false otherwise</returns>
        public bool AddObject(object obj)
        {
            if (objCount == POOL_SIZE)
                return false;

            lock (objLock)
            {
                objPool.Add(obj.GetHashCode(), obj);
                poolQueue.Enqueue(obj);
                objCount++;
            }

            return true;
        }

        /// <summary>
        /// Releases an object from the pool at the end of the queue
        /// </summary>
        /// <returns>The object if success, null otherwise</returns>
        public object ReleaseObject()
        {
            if (objCount == 0)
                return null;

            lock (objLock)
            {
                objPool.Remove(poolQueue.Dequeue().GetHashCode());
                objCount--;
                if (poolQueue.Count > 0)
                    return poolQueue.Dequeue();
            }

            return null;
        }

        /// <summary>
        /// Releases an object from the pool
        /// </summary>
        /// <param name="obj">Object to remove from the pool</param>
        /// <returns>The object if success, null otherwise</returns>
        public object ReleaseObject(object obj)
        {
            if (objCount == 0)
                return null;

            lock (objLock)
            {
                objPool.Remove(obj.GetHashCode());
                objCount--;
                RePopulate();
                return obj;
            }
        }

        /// <summary>
        /// Method that repopulates the 
        ///Queue after an object has been removed from the pool.
        /// This is done to make the queue 
        ///objects in sync with the objects in the hash table.
        /// </summary>
        private void RePopulate()
        {
            if (poolQueue.Count > 0)
                poolQueue.Clear();

            foreach (int key in objPool.Keys)
            {
                poolQueue.Enqueue(objPool[key]);
            }
        }

        /// <summary>
        /// Property that represents the current no of objects in the pool
        /// </summary>
        public int CurrentObjectsInPool
        {
            get
            {
                return objCount;
            }
        }

        /// <summary>
        /// Property that represents the maximum no of objects in the pool
        /// </summary>
        public int MaxObjectsInPool
        {
            get
            {
                return POOL_SIZE;
            }
        }
    }
}
