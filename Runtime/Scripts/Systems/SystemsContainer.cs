
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace VideoJames.Core.Systems
{
    public class SystemsContainer : SerializedScriptableObject, ISystem
    {
        [SerializeField] private List<ISystem> systems = new List<ISystem>();

        public void Init()
        {
            systems.ForEach(x => x.Init());
        }

        public void Tick(float deltaTime)
        {
            systems.ForEach(x => x.Tick(deltaTime));
        }
    }
}