using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    enum AnimationTriger
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
                    var clips = c2.animator.GetCurrentAnimatorClipInfo(0);
                    c1.StartClip = clips[0].clip.name;
                    c2.animator.SetTrigger(c1.Animation.ToString());
                    c1.Run = true;
                }
                else
                {
                    var clips = c2.animator.GetCurrentAnimatorClipInfo(0);
                    if (c1.StartClip == clips[0].clip.name)
                        _world.RemoveComponent<ActionAnimationComponent>(entity);
                }
            }
        }
    }
}