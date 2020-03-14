using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// отслеживание мертвых сущностей
    /// </summary>
    
    sealed class GameOverSystem : IEcsRunSystem
    {
        //TODO надо переделать смерть сущности, чтобы не сразу исчезала
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;

        readonly EcsFilter<NPCDataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<NPCDataSheetComponent> _dataSheetEntities = null;


        void IEcsRunSystem.Run()
        {
            if (_worldStatus.GameStatus == GameStatus.LevelRun)
            {
                foreach (var i in _playerEntities)
                {
                    var c1 = _playerEntities.Get1[i];

                    if (c1.Stats.HealthPoint <= 0)
                    {
                        _worldStatus.GameStatus = GameStatus.GameOver;
                    }
                }

                foreach (var i in _dataSheetEntities)
                {
                    var c1 = _dataSheetEntities.Get1[i];

                    if (c1.Stats.HealthPoint <= 0)
                    {
                        _dataSheetEntities.Entities[i].RLDestoryGO();
                    }
                }
            }
        }
    }
}