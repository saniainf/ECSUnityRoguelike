using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class TestSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        InjectFields _injectField = null;
        EcsFilter<Position> _entity = null;

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
                _world.RemoveEntity(in _entity.Entities[item]);
            }

            foreach (var item in _injectField.Entities)
            {
                var c1 = _world.GetComponent<Position>(in item);
            }
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}