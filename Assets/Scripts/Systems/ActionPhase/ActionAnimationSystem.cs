using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление анимацией чара в фазу действия
    /// </summary>
    [EcsInject]
    sealed class ActionAnimationSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionAnimationComponent, AnimationComponent> _animationEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _animationEntities)
            {
                ref var entity = ref _animationEntities.Entities[i];
                var c1 = _animationEntities.Components1[i];
                var c2 = _animationEntities.Components2[i];

                if (!c1.Run)
                {
                    c2.animator.SetTrigger(c1.Animation.ToString());
                    c1.Run = true;
                }
                else if (!c2.animator.GetBool(AnimatorField.ActionRun.ToString()))
                {
                    _world.RemoveComponent<ActionAnimationComponent>(entity);
                }
            }
        }
    }
}