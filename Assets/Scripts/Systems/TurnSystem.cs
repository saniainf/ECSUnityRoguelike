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
        int thisTurnEntityId = 0;

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

            //thisTurnEntity = npc[thisTurnEntityId];
            //Specify thisTurnEntitySpecify = _world.GetComponent<Specify>(thisTurnEntity);
            //thisTurnEntitySpecify.Status = Status.ACTION;
            _injectFields.thisTurnEntity = thisTurnEntity;

            _world.RemoveEntity(in thisTurnEntity);
        }

        void IEcsRunSystem.Run()
        {
            //npc.RemoveAll(entity => _world.IsEntityExists(in entity) == false);
            //Specify thisTurnEntitySpecify = _world.GetComponent<Specify>(thisTurnEntity);
            //if (thisTurnEntitySpecify.Status == Status.STANDBY)
            //{
            //    thisTurnEntity = npc[thisTurnEntityId + 1 > npc.Count ? 0 : thisTurnEntityId + 1];
            //}
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}