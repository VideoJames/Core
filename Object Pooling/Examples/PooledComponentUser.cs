using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using VideoJames.Core.Pooling;

namespace VideoJames.Examples.Core.Pooling
{
    public class PooledComponentUser : MonoBehaviour
    {
        [SerializeField]
        private PooledComponent _pooledComponent;

        [SerializeField]
        private int _poolCount;

        [Button("Add to pool")]
        private void AddToPool() => PoolManager.AddToPool(_pooledComponent, _poolCount);

        private void Awake()
        {
            AddToPool();
        }

        private void Update()
        {
            var pooledComponent = PoolManager.TakeNextObject(_pooledComponent);
            pooledComponent.transform.position = new Vector3(
                Random.Range(-50, 50),
                Random.Range(-50, 50),
                Random.Range(-50, 50));
        }
    }
}