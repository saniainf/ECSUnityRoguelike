using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class TestsSystem : IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        void IEcsInitSystem.Initialize()
        {
            for (int i = 0; i < 3; i++)
            {
                var e = _world.CreateEntityWith(out PlayerComponent playerComponent, out DataSheetComponent dataSheetComponent);
                dataSheetComponent.Initiative = 10;
            }
            for (int i = 0; i < 10; i++)
            {
                var e = _world.CreateEntityWith(out EnemyComponent enemyComponent, out DataSheetComponent dataSheetComponent);
                dataSheetComponent.Initiative = 5;
            }
        }

        void IEcsInitSystem.Destroy() { }
    }
}