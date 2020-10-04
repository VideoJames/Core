using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoJames.Core.Pooling
{
    public interface ICanBePooled
    {
        void OnAddedToPool();
        void OnTakenFromPool();
    }
}


