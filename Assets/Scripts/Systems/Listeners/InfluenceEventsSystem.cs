using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class InfluenceEventsSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ImpactEvent, DataSheetComponent> _impactEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _impactEntities)
            {
                ref var entity = ref _impactEntities.Entities[i];
                var c1 = _impactEntities.Components1[i];
                var c2 = _impactEntities.Components2[i];

                c2.HealthPoint -= c1.HitValue;
                _world.EnsureComponent<ActionAnimationComponent>(entity, out _).Animation = AnimatorField.AnimationTakeDamage;

                if (c2.HealthPoint <= 0)
                {
                    _world.RLRemoveGOEntity(entity, 0.3f);
                }
            }
        }
    }
}