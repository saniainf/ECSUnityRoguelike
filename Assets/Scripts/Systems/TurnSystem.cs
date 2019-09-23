using Leopotam.Ecs;
using System.Collections.Generic;

namespace Client
{
    [EcsInject]
    sealed class TurnSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsWorld _world = null;
        InjectFields _injectFields = null;
        EcsFilter<Specify, Player> _playerEntity = null;
        EcsFilter<Specify, Enemy> _enemyEntity = null;

        List<EcsEntity> npc;

        EcsEntity thisTurnEntity;

        void IEcsInitSystem.Initialize()
        {
            if (npc == null)
            {
                npc = new List<EcsEntity>(10);

                foreach (var i in _playerEntity)
                {
                    npc.Add(_playerEntity.Entities[i]);
                }

                foreach (var i in _enemyEntity)
                {
                    npc.Add(_enemyEntity.Entities[i]);
                }
            }

            thisTurnEntity = npc[0];
            Specify specify = _world.GetComponent<Specify>(thisTurnEntity);
            specify.Status = Status.ACTION;
            _injectFields.thisTurnEntity = thisTurnEntity;
        }

        void IEcsRunSystem.Run()
        {
            _world.IsEntityExists(in _injectFields.thisTurnEntity);

        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}