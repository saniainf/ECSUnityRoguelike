using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionSystem : IEcsRunSystem
    {
        // Auto-injected fields.
        EcsWorld _world = null;

        void IEcsRunSystem.Run()
        {
            
        }
    }
}