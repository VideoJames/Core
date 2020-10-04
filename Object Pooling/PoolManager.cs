
using System.Collections.Generic;
using UnityEngine;

namespace VideoJames.Core.Pooling
{
    public static class PoolManager
    {
        private static Dictionary<Component, Pool<Component>> _pools = new Dictionary<Component, Pool<Component>>();

        public static Pool<T> AddToPool<T>(T key, int pooledObjectCount) where T : Component
        {
            if (_pools.TryGetValue(key, out Pool<Component> pool))
            {
                pool.IncreaseCount(pooledObjectCount);
            }
            else
            {
                pool = new Pool<Component>(key, pooledObjectCount);
                _pools.Add(key, pool);
            }
            return pool as Pool<T>;
        }

        public static T TakeNextObject<T>(T key) where T : Component
        {
            if(_pools.TryGetValue(key, out var pool))
            {
                return pool.TakeNextObject() as T;
            }
            throw new System.Exception($"Trying to get pooled object before creating a pool.");
        }
    }
}

