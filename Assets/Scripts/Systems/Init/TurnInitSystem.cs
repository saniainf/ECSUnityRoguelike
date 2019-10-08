using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TurnInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        void IEcsInitSystem.Initialize()
        {
            _world.CreateEntityWith(out WorldCreateEvent _);
        }

        void IEcsInitSystem.Destroy() { }

        void IEcsRunSystem.Run()
        {
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                _world.CreateEntityWith(out WorldCreateEvent _);
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                _world.CreateEntityWith(out WorldDestroyEvent _);
            }
        }
    }
}