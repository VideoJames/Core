
using UnityEngine;

namespace VideoJames.Core.Pooling
{
    [System.Serializable]
    public class PoolSettings
    {
        public Component PooledComponent;
        public int Count;
    }
}