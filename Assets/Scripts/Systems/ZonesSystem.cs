using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class ZonesSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<PositionComponent, ZoneExitComponent> _zoneExitEntities = null;

        readonly WorldStatus _worldStatus = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                var pc1 = _playerEntities.Components1[i];

                foreach (var j in _zoneExitEntities)
                {
                    var zc1 = _zoneExitEntities.Components1[i];

                    if (pc1.Coords == zc1.Coords)
                    {
                        _worldStatus.GameStatus = GameStatus.LevelEnd;
                    }
                }
            }
        }
    }
}