
using System;

namespace VideoJames.Core.Pooling
{
    public interface IPooledObject<T> where T : IPooledObject<T>
    {
        T Key { get; set; }
        void OnReturnedToPool();
        void OnRetrievedFromPool();
        Func<T> CreateObjectMethod { get; }
    }
}