using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class CollectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, ActionPhaseComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;
        readonly EcsFilter<PositionComponent, FoodComponent>.Exclude<GameObjectRemoveEvent> _foodEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                ref var pe = ref _playerEntities.Entities[i];
                var pc1 = _playerEntities.Components1[i];

                foreach (var j in _foodEntities)
                {
                    ref var fe = ref _foodEntities.Entities[j];
                    var fc1 = _foodEntities.Components1[j];
                    var fc2 = _foodEntities.Components2[j];

                    if (pc1.Coords == fc1.Coords)
                    {
                        var c = _world.EnsureComponent<CollectEvent>(pe, out _);
                        c.HealValue = fc2.foodValue;

                        _world.AddComponent<GameObjectRemoveEvent>(fe);
                    }
                }
            }
        }
    }
}