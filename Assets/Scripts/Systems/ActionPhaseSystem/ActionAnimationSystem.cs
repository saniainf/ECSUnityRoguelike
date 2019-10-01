using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    enum ActionAnimation
    {
        NONE,
        IDLE,
        CHOP,
        HIT
    }

    [EcsInject]
    sealed class ActionAnimationSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionAnimationComponent, AnimationComponent>.Exclude<GameObjectRemoveEvent> _animationEntities = null;

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
                else
                {
                    var state = c2.animator.GetCurrentAnimatorStateInfo(0);
                    if (state.IsName(ActionAnimation.IDLE.ToString()))
                    {
                        _world.RemoveComponent<ActionAnimationComponent>(entity);
                    }
                }
            }
        }
    }
}