using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class AppearanceSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<AnimationComponent, DataSheetComponent>.Exclude<GameObjectRemoveEvent> _aspectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _aspectEntities)
            {
                var c1 = _aspectEntities.Components1[i];
                var c2 = _aspectEntities.Components2[i];
                var cur = c1.animator.GetBool("Damaged");
                if ((c2.HealthPoint != c2.CurrentHealthPoint) != cur)
                {
                    c1.animator.SetBool("Damaged", true);
                }
            }
        }
    }
}