using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionSystem : IEcsRunSystem
    {
        EcsWorld _world = null;

        void IEcsRunSystem.Run()
        {

        }
    }
}