using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Client
{
    /// <summary>
    /// передача хода следующему чару в очереди
    /// </summary>
    [EcsInject]
    sealed class NextTurnSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<TurnComponent> _canTurnEntities = null;
        readonly EcsFilter<InputPhaseComponent> _inputPhaseEntities = null;
        readonly EcsFilter<ActionPhaseComponent> _actionPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            if (_canTurnEntities.GetEntitiesCount() > 0 && _inputPhaseEntities.GetEntitiesCount() == 0 && _actionPhaseEntities.GetEntitiesCount() == 0)
                nextEntity();
        }

        void nextEntity()
        {
            var entity = new EcsEntity();
            var queue = int.MaxValue;

            foreach (var i in _canTurnEntities)
            {
                if (_canTurnEntities.Components1[i].Queue < queue)
                {
                    queue = _canTurnEntities.Components1[i].Queue;
                    entity = _canTurnEntities.Entities[i];
                }
            }

            _world.AddComponent<InputPhaseComponent>(entity);
        }
    }
}