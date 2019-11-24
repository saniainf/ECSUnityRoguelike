using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление внешним видом всего что может повреждаться
    /// </summary>
    [EcsInject]
    sealed class AppearanceSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _aspectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _aspectEntities)
            {
                var c1 = _aspectEntities.Components1[i];
                var c2 = _aspectEntities.Components2[i];

                c1.GOcomps.Animator.SetBool(AnimatorField.Damaged.ToString(), (c2.Stats.MaxHealthPoint != c2.Stats.HealthPoint));
            }
        }
    }
}