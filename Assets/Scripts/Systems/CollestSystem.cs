using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class CollestSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<PositionComponent, FoodComponent> _foodEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                var pc1 = _playerEntities.Components1[i];
                var pc2 = _playerEntities.Components2[i];

                foreach (var j in _foodEntities)
                {
                    ref var fe = ref _foodEntities.Entities[j];
                    var fc1 = _foodEntities.Components1[j];
                    var fc2 = _foodEntities.Components2[j];

                    if (pc1.Coords == fc1.Coords)
                    {
                        pc2.FoodPoint += fc2.foodValue;
                        _world.AddComponent<GameObjectRemoveEvent>(fe);
                    }
                }
            }
        }
    }
}