using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TurnInitSystem : IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<PlayerComponent, TurnComponent> _player = null;

        void IEcsInitSystem.Initialize()
        {
            foreach (var i in _player)
            {
                _player.Components2[i].Phase = Phase.INPUT;
                _world.AddComponent<InputPhaseComponent>(in _player.Entities[i]);
            }
        }

        void IEcsInitSystem.Destroy() { }
    }
}