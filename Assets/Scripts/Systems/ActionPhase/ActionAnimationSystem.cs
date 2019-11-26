using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление анимацией чара в фазу действия
    /// </summary>
    
    sealed class ActionAnimationSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionAnimationComponent, GameObjectComponent> _animationEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _animationEntities)
            {
                ref var e = ref _animationEntities.Entities[i];
                var c1 = _animationEntities.Get1[i];
                var c2 = _animationEntities.Get2[i];

                if (!c1.Run)
                {
                    c2.GOcomps.Animator.SetTrigger(c1.Animation.ToString());
                    c1.Run = true;

                    Debug.Log($"entity: {e.GetInternalId()} | запущена action анимация: {c1.Animation.ToString()}");
                }
                else if (!c2.GOcomps.Animator.GetBool(AnimatorField.ActionRun.ToString()))
                {
                    e.Unset<ActionAnimationComponent>();
                }
            }
        }
    }
}