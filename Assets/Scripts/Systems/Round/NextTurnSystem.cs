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

    sealed class NextTurnSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;

        readonly EcsFilter<TurnComponent> _canTurnEntities = null;
        readonly EcsFilter<InputPhaseComponent> _inputPhaseEntities = null;
        readonly EcsFilter<ActionPhaseComponent> _actionPhaseEntities = null;
        readonly EcsFilter<ModificationPhaseComponent> _environmentPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            if (_canTurnEntities.GetEntitiesCount() > 0
                && _inputPhaseEntities.GetEntitiesCount() == 0
                && _actionPhaseEntities.GetEntitiesCount() == 0
                && _environmentPhaseEntities.GetEntitiesCount() == 0)
                nextEntity();
        }

        void nextEntity()
        {
            var entity = new EcsEntity();
            var queue = int.MaxValue;

            foreach (var i in _canTurnEntities)
            {
                if (_canTurnEntities.Get1[i].Queue < queue)
                {
                    queue = _canTurnEntities.Get1[i].Queue;
                    entity = _canTurnEntities.Entities[i];
                }
            }

            entity.Set<InputPhaseComponent>();
            _worldStatus.PlayerTurnSet(entity.Get<PlayerComponent>() != null);
        }
    }
}