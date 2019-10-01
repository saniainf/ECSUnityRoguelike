using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class EnemyInputSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, EnemyComponent>.Exclude<GameObjectRemoveEvent> _inputPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            var direction = VExt.NextEnum<MoveDirection>();

            foreach (var i in _inputPhaseEntities)
            {
                ref var entity = ref _inputPhaseEntities.Entities[i];
                var c = _world.AddComponent<InputDirectionComponent>(entity);
                c.MoveDirection = direction;
                _world.AddComponent<PhaseEndEvent>(entity);
            }
        }
    }
}
