using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class InfluenceEventsSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ImpactEvent, DataSheetComponent> _impactEntities = null;
        readonly EcsFilter<CollectEvent, DataSheetComponent> _collectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _impactEntities)
            {
                ref var entity = ref _impactEntities.Entities[i];
                var c1 = _impactEntities.Components1[i];
                var c2 = _impactEntities.Components2[i];

                c2.CurrentHealthPoint -= c1.HitValue;
                _world.EnsureComponent<ActionAnimationComponent>(entity, out _).Animation = AnimationTriger.AnimationTakeDamage;

                if (c2.CurrentHealthPoint <= 0)
                {
                    var c = _world.AddComponent<GameObjectRemoveEvent>(entity);
                    c.RemoveTime = 0.3f;
                }
            }

            foreach (var i in _collectEntities)
            {
                ref var entity = ref _collectEntities.Entities[i];
                var c1 = _collectEntities.Components1[i];
                var c2 = _collectEntities.Components2[i];

                c2.HealthPoint += c1.BoostHealthValue;
                c2.CurrentHealthPoint = Mathf.Min(c2.CurrentHealthPoint + c1.HealValue + c1.BoostHealthValue, c2.HealthPoint);
            }
        }
    }
}