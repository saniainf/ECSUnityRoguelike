using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class QueueSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<TurnComponent> _canTurnEntities = null;
        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<DataSheetComponent, EnemyComponent> _enemyEntities = null;

        void IEcsRunSystem.Run()
        {
            if (_canTurnEntities.GetEntitiesCount() == 0 && (_enemyEntities.GetEntitiesCount() > 0 || _playerEntities.GetEntitiesCount() > 0))
            {
                NewRoundQueue();
            }
        }

        void NewRoundQueue()
        {
            var sortedEntities = new List<Tuple<int, EcsEntity>>();
            int queue = 0;

            foreach (var i in _playerEntities)
            {
                sortedEntities.Add(new Tuple<int, EcsEntity>(_playerEntities.Components1[i].Initiative, _playerEntities.Entities[i]));
            }

            foreach (var i in _enemyEntities)
            {
                sortedEntities.Add(new Tuple<int, EcsEntity>(_enemyEntities.Components1[i].Initiative, _enemyEntities.Entities[i]));
            }

            sortedEntities.Sort((a, b) => b.Item1.CompareTo(a.Item1));

            foreach (var e in sortedEntities)
            {
                var c = _world.AddComponent<TurnComponent>(e.Item2);
                c.Queue = queue++;
            }
        }
    }
}