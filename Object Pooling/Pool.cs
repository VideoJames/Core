
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VideoJames.Core.Pooling
{
    public class Pool<T> where T : Component
    {
        public int Count => _pooledObjects.Count;

        private Queue<T> _pooledObjects = new Queue<T>();
        private T _pooledObjectPrefab;

        public Pool(T pooledObjectPrefab, int count)
        {
            _pooledObjectPrefab = pooledObjectPrefab;
            IncreaseCount(count);
        }

        public T TakeNextObject()
        {
            T pooledObject = _pooledObjects.Dequeue();
            while (pooledObject == null && _pooledObjects.Count > 0)
            {
                pooledObject = _pooledObjects.Dequeue();
            }
            _pooledObjects.Enqueue(pooledObject);
            CallPooledObjectMethod(pooledObject, MethodType.OnTaken);
            return pooledObject;
        }

        public void IncreaseCount(int increase)
        {
            for (var i = 0; i < increase; i++)
            {
                T pooledObject = Object.Instantiate(_pooledObjectPrefab, Vector3.one * -1000f, Quaternion.identity);
                _pooledObjects.Enqueue(pooledObject);
                CallPooledObjectMethod(pooledObject, MethodType.OnAdded);
            }
        }

        private enum MethodType { OnAdded, OnTaken }
        private static void CallPooledObjectMethod(T pooledObject, MethodType methodType)
        {
            var pooledComponent = pooledObject.GetComponent<ICanBePooled>();
            if (pooledComponent == null) return;

            switch (methodType)
            {
                case MethodType.OnAdded:
                    pooledComponent.OnAddedToPool();
                    break;
                case MethodType.OnTaken:
                    pooledComponent.OnTakenFromPool();
                    break;
            }
        }
    }
}