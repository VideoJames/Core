﻿
using System.Collections.Generic;

namespace VideoJames.Core.Pooling
{
    public static class Pool<T> where T : IPooledObject<T>
    {
        private static Dictionary<T, Queue<T>> poolQueues = new Dictionary<T, Queue<T>>();

        public static T GetNextObject(T key)
        {
            var nextObject = poolQueues[key].Dequeue();
            return nextObject;            
        }

        public static void ReturnObject(T returnedObject)
        {
            poolQueues[returnedObject.Key].Enqueue(returnedObject);
            returnedObject.OnReturnedToPool();
        }

        public static void Reserve(T key, int count)
        {
            if (!poolQueues.ContainsKey(key))
            {
                poolQueues.Add(key, new Queue<T>(count));
            }
            var queue = poolQueues[key];
            for (var i = 0; i < count; i++)
            {
                var clonedObject = key.CreateObjectMethod.Invoke();
                clonedObject.Key = key;
                queue.Enqueue(clonedObject);
            }
        }
    }
}