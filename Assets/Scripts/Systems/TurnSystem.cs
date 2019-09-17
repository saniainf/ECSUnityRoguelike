using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class TurnSystem : IEcsRunSystem
    {
        EcsWorld _world = null;
        InjectFields _injectFields = null;

        EcsFilter<Turn> _turnEntity = null;

        EcsEntity thisTurnEntity;

        void IEcsRunSystem.Run()
        {
            thisTurnEntity = _injectFields.thisTurnEntity;
            // if turn this entity over
            // next turn entity
            // _turnEntity
        }
    }
}