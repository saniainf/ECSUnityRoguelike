using Leopotam.Ecs;
using System.Collections.Generic;

namespace Client
{
    [EcsInject]
    sealed class TurnSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsWorld _world = null;
        InjectFields _injectFields = null;
        EcsFilter<SpecifyComponent, Player> _playerEntity = null;
        EcsFilter<SpecifyComponent, Enemy> _enemyEntity = null;

        void IEcsInitSystem.Initialize()
        {

        }

        void IEcsRunSystem.Run()
        {

        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}