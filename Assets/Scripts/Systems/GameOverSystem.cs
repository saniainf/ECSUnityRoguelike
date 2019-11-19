using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class GameOverSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<DataSheetComponent> _dataSheetEntities = null;


        void IEcsRunSystem.Run()
        {
            if (_worldStatus.GameStatus == GameStatus.LevelRun)
            {
                foreach (var i in _playerEntities)
                {
                    var c1 = _playerEntities.Components1[i];

                    if (c1.Stats.HealthPoint <= 0)
                    {
                        _worldStatus.GameStatus = GameStatus.GameOver;
                    }
                }

                foreach (var i in _dataSheetEntities)
                {
                    var c1 = _dataSheetEntities.Components1[i];

                    if (c1.Stats.HealthPoint <= 0)
                    {
                        _world.RLRemoveGOEntity(_dataSheetEntities.Entities[i]);
                    }
                }
            }
        }
    }
}