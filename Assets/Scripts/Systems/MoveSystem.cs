using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class MoveSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        void IEcsRunSystem.Run()
        {

        }
    }
}