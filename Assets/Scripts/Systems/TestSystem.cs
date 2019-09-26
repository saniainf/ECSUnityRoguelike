using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class TestSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        void IEcsRunSystem.Run()
        {

        }

    }
}