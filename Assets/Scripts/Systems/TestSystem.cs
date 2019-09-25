using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class TestSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        InjectFields _injectField = null;
        EcsFilter<PositionComponent> _entity = null;

        void IEcsInitSystem.Initialize()
        {
            foreach (var item in _entity)
            {
                _injectField.Entities.Add(_entity.Entities[item]);
            }
        }

        void IEcsRunSystem.Run()
        {
            foreach (var item in _entity)
            {
                _world.AddComponent<GameObjectRemoveEvent>(in _entity.Entities[item]);
            }
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}