using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление внешним видом всего что может повреждаться
    /// </summary>
    
    sealed class AppearanceSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _aspectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _aspectEntities)
            {
                var c1 = _aspectEntities.Get1[i];
                var c2 = _aspectEntities.Get2[i];

                c1.GObj.Animator.SetBool(AnimatorField.Damaged.ToString(), (c2.Stats.MaxHealthPoint != c2.Stats.HealthPoint));
            }
        }
    }
}