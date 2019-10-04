using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class InfluenceSystem : IEcsRunSystem
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

                if (c2.HealthPoint <= 0)
                {
                    var c = _world.AddComponent<GameObjectRemoveEvent>(entity);
                    c.RemoveTime = 0.3f;
                }
            }
        }
    }
}