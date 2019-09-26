using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class TestInitSystem : IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<PlayerComponent> _player = null;

        void IEcsInitSystem.Initialize()
        {
            _world.AddComponent<InputPhaseComponent>(in _player.Entities[0]);
        }

        void IEcsInitSystem.Destroy() { }
    }
}