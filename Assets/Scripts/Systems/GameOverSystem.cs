using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class GameOverSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PlayerComponent> _playerEntities = null;

        readonly EcsFilter<WorldCreateEvent> _worldCreateEvent = null;
        readonly EcsFilter<WorldDestroyEvent> _worldDestroyEvent = null;

        bool levelRun = false;

        void IEcsRunSystem.Run()
        {
            if (_worldCreateEvent.GetEntitiesCount() > 0)
                levelRun = true;

            if (_worldDestroyEvent.GetEntitiesCount() > 0)
                levelRun = false;

            if (levelRun && _playerEntities.GetEntitiesCount() == 0)
            {
                _world.CreateEntityWith(out WorldDestroyEvent _);
                _world.CreateEntityWith(out UIDisableEvent uIDisable);
                uIDisable.UIType = UIType.LevelRun;

                _world.CreateEntityWith(out UIEnableEvent uIEnable);
                uIEnable.UIType = UIType.GameOver;
            }
        }
    }
}