using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// контроль специальных зон на карте, выход и т.д.
    /// </summary>
    [EcsInject]
    sealed class ZonesSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<GameObjectComponent, ZoneExitComponent> _zoneExitEntities = null;

        readonly WorldStatus _worldStatus = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                var pc1 = _playerEntities.Components1[i];

                foreach (var j in _zoneExitEntities)
                {
                    var zc1 = _zoneExitEntities.Components1[i];

                    if (pc1.Transform.position == zc1.Transform.position)
                    {
                        _worldStatus.GameStatus = GameStatus.LevelEnd;
                    }
                }
            }
        }
    }
}