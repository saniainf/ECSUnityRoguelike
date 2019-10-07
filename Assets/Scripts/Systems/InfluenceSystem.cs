using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class InfluenceSystem : IEcsRunSystem
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

                c2.HealthPoint -= c1.HitValue;
                Debug.Log("damage " + c1.HitValue);

                if (c2.HealthPoint <= 0)
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

                c2.HealthPoint += c1.HealValue;
                Debug.Log("heal " + c1.HealValue);
            }
        }
    }
}