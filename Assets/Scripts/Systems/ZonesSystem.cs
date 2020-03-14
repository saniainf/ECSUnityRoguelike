using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// контроль специальных зон на карте, выход и т.д.
    /// </summary>

    sealed class ZonesSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<GameObjectComponent, ExitPointComponent> _zoneExitEntities = null;

        readonly WorldStatus _worldStatus = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                var pc1 = _playerEntities.Get1[i];

                foreach (var j in _zoneExitEntities)
                {
                    var zc1 = _zoneExitEntities.Get1[i];

                    if (zc1.GO.Collider.OverlapPoint(pc1.Transform.position))
                    {
                        _worldStatus.GameStatus = GameStatus.LevelEnd;
                    }
                }
            }
        }
    }
}