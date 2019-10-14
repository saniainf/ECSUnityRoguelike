using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;

namespace Client
{
    [EcsInject]
    sealed class NextTurnSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<TurnComponent> _canTurnEntities = null;
        readonly EcsFilter<InputPhaseComponent> _inputPhaseEntities = null;
        readonly EcsFilter<ActionPhaseComponent> _actionPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            if (_canTurnEntities.GetEntitiesCount() != 0 && _inputPhaseEntities.GetEntitiesCount() == 0 && _actionPhaseEntities.GetEntitiesCount() == 0)
                nextEntity();
        }

        void nextEntity()
        {
            int queue = 0;
            ref var entity = ref _canTurnEntities.Entities[0];

            foreach (var i in _canTurnEntities)
            {
                ref var e = ref _canTurnEntities.Entities[i];
                var c1 = _canTurnEntities.Components1[i];

                if (c1.Queue > queue)
                {
                    queue = c1.Queue;
                    entity = e;
                }
            }

            _world.AddComponent<InputPhaseComponent>(entity);
        }
    }
}