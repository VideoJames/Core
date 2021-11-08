
using Sirenix.OdinInspector;
using UnityEngine;

namespace VideoJames.Core.Systems
{
    /// <summary>
    /// A monobehaviour object responsible for initializing the overarching systems used in this game.
    /// </summary>
    public class SystemsBehaviour : MonoBehaviour
    {
        [SerializeField, Required] private SystemsContainer systems;
    }
}