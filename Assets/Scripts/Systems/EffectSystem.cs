using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class EffectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<SpriteEffectComponent> _effectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _effectEntities)
            {
                ref var entity = ref _effectEntities.Entities[i];
                var c1 = _effectEntities.Components1[i];

                c1.LifeTime -= Time.deltaTime;

                if (c1.LifeTime <= 0)
                {
                    _world.AddComponent<GameObjectRemoveEvent>(entity);
                }
            }
        }
    }
}