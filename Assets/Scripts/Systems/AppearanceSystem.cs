using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class AppearanceSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<AnimationComponent, DataSheetComponent> _aspectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _aspectEntities)
            {
                var c1 = _aspectEntities.Components1[i];
                var c2 = _aspectEntities.Components2[i];

                c1.animator.SetBool(AnimatorField.Damaged.ToString(), (c2.MaxHealthPoint != c2.HealthPoint));
            }
        }
    }
}