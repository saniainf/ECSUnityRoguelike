using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using System;

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
            if (_canTurnEntities.GetEntitiesCount() > 0 && _inputPhaseEntities.GetEntitiesCount() == 0 && _actionPhaseEntities.GetEntitiesCount() == 0)
                nextEntity();
        }

        void nextEntity()
        {
            var sortedEntities = new List<Tuple<int, EcsEntity>>();

            foreach (var i in _canTurnEntities)
            {
                sortedEntities.Add(new Tuple<int, EcsEntity>(_canTurnEntities.Components1[i].Queue, _canTurnEntities.Entities[i]));
            }

            sortedEntities.Sort((a, b) => a.Item1.CompareTo(b.Item1));

            _world.AddComponent<InputPhaseComponent>(sortedEntities[0].Item2);
        }
    }
}